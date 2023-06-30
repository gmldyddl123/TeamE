using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace player
{
    enum State
    {
        IDLE = 0,
        WALK,
        RUN,
        SPRINT,
        InAir,
        Paragliding,
        SlowDown,
        Attack
    }
    public class PlayerInputSystem : MonoBehaviour
    {
        //// 대화중인지 아닌지
        //public bool isDialogue = false;

        //// 대화 프로퍼티
        //// 대화중이면 플레이어 액션을 끊음
        //// 대화중이 아니면 플레이어 액션을 연결
        //public bool IsDialogue
        //{
        //    get => isDialogue;
        //    set
        //    {
        //        if (isDialogue)
        //        {
        //            inputActions.Player.Disable();
        //        }
        //        if (!isDialogue)
        //        {
        //            inputActions.Player.Enable();
        //        }
        //    }
        //}


        //컴퍼넌트
        //Rigidbody playerRigidbody;
        PlayerInputAction inputActions;
        public CharacterController characterController;
        Animator animator;


        //현재 상태
        public PlayerState playerCurrentStates;
        PlayerState idleState;
        PlayerState walkState;
        PlayerState runState; 
        PlayerState sprintState;
        //PlayerState jumpState; 점프는 InAir로 합병
        PlayerState inAirState;
        PlayerState paraglidingState;
        PlayerState slowDownState ;
        PlayerState attackState;

        //애니메이션
        //readonly int InputYString = Animator.StringToHash("InputY");
        readonly int AnimatorState = Animator.StringToHash("State");

        //걷기
        bool walkBool = false;

        //입력값
        private Vector2 movementInput; //액션으로 받는 입력값
        private Vector3 moveDir; //입력값으로 만든 벡터3

        //캐릭터 컨트롤러
        float gravity = -9.81f; // 중력
        public Vector3 moveDirection; // 카메라까지 계산한 이동 방향

        //회전
        Transform cameraObject;
        Vector3 targetDirection = Vector3.zero; //회전하는 방향
        
        //점프 낙하
        public float lastMemorySpeed = 0.0f;
        bool isInAir = false;

        int groundLayer;
        bool fallingDirYSetComplete = false;

        //패러 글라이딩
        bool isParagliding = false;
        private float rotationSpeed = 2f;

        //공격
        bool isAttack  = false;


        private void Awake()
        {
            //playerRigidbody = GetComponent<Rigidbody>();
            inputActions = new PlayerInputAction();
            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            cameraObject = Camera.main.transform;


            //상태
            idleState = new IdleState(this);
            walkState = new WalkState(this);
            runState = new RunState(this);
            sprintState = new SprintState(this);
            //jumpState = new JumpState(this, characterController);
            inAirState = new InAirState(this, characterController);
            paraglidingState = new ParaglidingState(this, characterController);
            slowDownState = new SlowDownState(this);
            attackState = new AttackState(this, animator);
            

            //레이어 
            groundLayer = 1 << LayerMask.NameToLayer("Ground");

            playerCurrentStates = idleState;
            //playerCurrentStates = slowDownState;
            // 커서 락
            //Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnEnable()
        {

            //인풋시스템
            inputActions.Player.Enable();
            
            //WASD
            inputActions.Player.Movement.performed += MovementLogic;
            inputActions.Player.Movement.canceled += MovementLogic;

            //Shift 전력 질주
            inputActions.Player.Sprint.performed += SprintButton;
            //inputActions.Player.CameraLook.performed += i => cameraInput = i.ReadValue<Vector2>();

            //Control 걷기
            inputActions.Player.Walk.performed += WalkButton;

            //Space 점프
            inputActions.Player.Jump.performed += JumpButton;

            //마우스 좌클릭 공격
            inputActions.Player.Attack.performed += AttackButton;

        }

        private void AttackButton(InputAction.CallbackContext obj)
        {
            attackState.EnterState();
        }

        private void JumpButton(InputAction.CallbackContext _)
        {
            if (!isInAir)
            {
                inAirState.EnterState();
                isInAir = true;
                moveDirection.y = 3f;
            }
            else
            {
                if (isParagliding)
                {
                    isParagliding = false;
                    inAirState.EnterState();
                }
                else
                {
                    if (!Physics.Raycast(transform.position, Vector3.down, characterController.height * 1.5f, groundLayer))
                    {
                        isParagliding = true;
                        paraglidingState.EnterState();
                        
                    }
                }
            }
        }

        private void WalkButton(InputAction.CallbackContext _)
        {
            walkBool = walkBool ? false : true;
            if (walkBool && movementInput != Vector2.zero)
                walkState.EnterState();
            else if (!walkBool && movementInput != Vector2.zero)
                runState.EnterState();
        }

        private void SprintButton(InputAction.CallbackContext _)
        {
            if(movementInput != Vector2.zero && !isInAir)
            {
                sprintState.EnterState();
                walkBool = false;
            }
        }

        private void MovementLogic(InputAction.CallbackContext context)
        {
            //if (isJumping)
            //    return;

            movementInput = context.ReadValue<Vector2>();
            moveDir.x = movementInput.x;
            moveDir.z = movementInput.y;

            if(!isInAir)
            {
                if (movementInput == Vector2.zero)
                {
                    //idleState.EnterState();
                    slowDownState.EnterState();
                }
                else if (playerCurrentStates != sprintState && !walkBool)
                {
                    runState.EnterState();
                }
                else if (walkBool)
                {
                    walkState.EnterState();
                }
            }
           
        }


        private void OnDisable()
        {
            inputActions.Player.Disable();
        }

        //private void Update()
        //{
        //    if(playerCurrentStates == attackState)
        //    {
        //        Debug.Log(timer += Time.deltaTime);
        //    }
        //}

        private void FixedUpdate()
        {
            playerCurrentStates.MoveLogic();
        }


        public void PlayerMove(float moveSpeed)
        {
            if (characterController.isGrounded == false)
            {
                moveDirection.y += gravity * Time.fixedDeltaTime;

                if (!Physics.Raycast(transform.position, Vector3.down, characterController.height * 0.5f + 0.3f, groundLayer) && !fallingDirYSetComplete)
                {
                    fallingDirYSetComplete = true;
                    isInAir = true;
                    moveDirection.y = 0;
                    inAirState.EnterState();
                }
            }
            else
            {
                fallingDirYSetComplete = false;
            }
            
            characterController.Move(moveDirection * moveSpeed * Time.fixedDeltaTime);
        }


        public void UseGravity(float gravity = -9.81f)
        {
            if (characterController.isGrounded == false)
            {
                if(moveDirection.y > -10f)
                    moveDirection.y += gravity * Time.fixedDeltaTime;
                //moveDirection.y += gravity * Time.fixedDeltaTime;
            }
            else
            {
                isInAir = false;
                isParagliding = false;
                MoveToDir();
                if (movementInput == Vector2.zero)
                {
                    //slowDownState.EnterState();
                    idleState.EnterState();
                }
                else if (playerCurrentStates != sprintState && !walkBool)
                {
                    runState.EnterState();
                }
                else if (walkBool)
                {
                    walkState.EnterState();
                }
            }
        }
        public void TestLandingGroundCheck()
        {

            if (characterController.isGrounded)
            {
                isInAir = false;
                isParagliding = false;
                MoveToDir();
                if (movementInput == Vector2.zero)
                {
                    idleState.EnterState();
                }
                else if (playerCurrentStates != sprintState && !walkBool)
                {
                    runState.EnterState();
                }
                else if (walkBool)
                {
                    walkState.EnterState();
                }
            }
        }

        public void PlayerEnterIdleState()
        {
            idleState.EnterState();
        }
        public void PlayerAnimoatrChage(int state)
        {
            animator.SetInteger(AnimatorState, state);
        }

        
        public void MoveToDir()
        {

            //Vector3 movedis = cameraObject.rotation * new Vector3(moveDir.x, 0, moveDir.z);

            //moveDirection = new Vector3(movedis.x, moveDirection.y, movedis.z);

            /////////////////////////////////////////////////////////
           // Vector3 direction = new Vector3(movementInput.x, 0, movementInput.y);
            Vector3 direction = new Vector3(moveDir.x, 0, moveDir.z);
            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraObject.eulerAngles.y;

                //이동 방향
                Vector3 cameraAngleCalculation = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                moveDirection = new Vector3(cameraAngleCalculation.x, moveDirection.y, cameraAngleCalculation.z);
                moveDirection.Normalize();

                //회전
                Quaternion targerRotation = Quaternion.LookRotation(cameraAngleCalculation);
                transform.rotation = targerRotation;

                //스무스 회전
                //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                //transform.rotation = Quaternion.Euler(0.0f, angle, 0f);
            }

            //PlayerRotate();
        }


        private void PlayerRotate()
        {
            targetDirection = cameraObject.forward * moveDir.z;
            targetDirection = targetDirection + cameraObject.right * moveDir.x;
            targetDirection.Normalize();

            if (targetDirection == Vector3.zero)
                targetDirection = transform.forward;

            targetDirection.y = 0;


            Quaternion targerRotation = Quaternion.LookRotation(targetDirection);
            //Quaternion playerRoation = Quaternion.Slerp(transform.rotation, targerRotation, rotationSpeed * Time.fixedDeltaTime);

            transform.rotation = targerRotation;
            //transform.rotation = playerRoation;
        }

        public void PlayerRotateSlerp()//패러 글라딩이에 사용중 
        {
            Vector3 movedis = cameraObject.rotation * new Vector3(moveDir.x, 0, moveDir.z);

            moveDirection = new Vector3(movedis.x, moveDirection.y, movedis.z);


            targetDirection = cameraObject.forward * moveDir.z;
            targetDirection = targetDirection + cameraObject.right * moveDir.x;
            targetDirection.Normalize();

            if (targetDirection == Vector3.zero)
                targetDirection = transform.forward;

            targetDirection.y = 0;


            Quaternion targerRotation = Quaternion.LookRotation(targetDirection);
            Quaternion playerRoation = Quaternion.Slerp(transform.rotation, targerRotation, rotationSpeed * Time.fixedDeltaTime);

            transform.rotation = targerRotation;
            transform.rotation = playerRoation;
        }
    }
}