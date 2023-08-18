using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;


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
    public class PlayerController : MonoBehaviour
    {
        //컴퍼넌트
        //Rigidbody playerRigidbody;
        PlayerInputAction inputActions;
        public CharacterController characterController;
        Animator animator;
        GameObject useCheck;

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
        public bool isAttack { get; set; } = false;
        //public bool attackMove { get; private set; } = false;

        //무기 소환

        public GameObject handWeapon;
        public GameObject backWeapon;

        /// <summary>
        /// 캐릭터 선택 폭
        /// </summary>
        //플레이어 스텟 각각의 공격과 무브 로직이 다르다
        public PlayerStat currentPlayerCharater; // 현재 선택된 캐릭터
        CapsuleCollider attackCollider; //선택된 캐릭터의 공격 콜라이더 위치 바꿔줘야함 과거의 잔재임
        const int maxPickCharacter = 2; // 최대 선택 캐릭터
        public PlayerStat[] pickChr = new PlayerStat[maxPickCharacter]; //테스트용 퍼블릭 고를 수있는 캐릭터들

        int currentPickCharacterNum = 0; //캐릭터 변경을 위한것 1번 2번 누르면 그 숫자로 변함
        public int CurrentPickCharacterNum
        {
            get => currentPickCharacterNum;
            set
            {
                if (currentPickCharacterNum != value)
                {
                    currentPickCharacterNum = value;
                    ChangeCharater(currentPickCharacterNum);
                }
            }
        }

        private void Awake()
        {
            //playerRigidbody = GetComponent<Rigidbody>();
            inputActions = new PlayerInputAction();
            characterController = GetComponent<CharacterController>();
            //animator = GetComponent<Animator>();
            cameraObject = Camera.main.transform;

            //선택한 캐릭터 관련 불러오기
            currentPlayerCharater = pickChr[0];
            //characterController = pickChr[0].GetComponent<CharacterController>();
            attackCollider = currentPlayerCharater.attackCollider;
            //현재 캐릭터의 오버라이드 애니메이터를 가져올 수 있다
            animator = pickChr[0].GetComponent<Animator>();
            animator.runtimeAnimatorController = currentPlayerCharater.animator;


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

            if(attackState != null)
            {
                AttackState at = attackState as AttackState;
                at.attackMove = currentPlayerCharater.Attack;
            }
            //attackState. += playerStat.attackCollider;

            //레이어 
            groundLayer = 1 << LayerMask.NameToLayer("Ground");

            playerCurrentStates = idleState;
            //playerCurrentStates = slowDownState;
            // 커서 락
            //Cursor.lockState = CursorLockMode.Locked;
            UseChecker checker = GetComponentInChildren<UseChecker>();
            useCheck = transform.GetChild(3).gameObject;
            checker.onItemUse += UseItem;
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

            //캐릭터 변경
            inputActions.Player.CharacterChange_1.performed += CharaterChangeButton_1;
            inputActions.Player.CharacterChange_2.performed += CharaterChangeButton_2;

            //상호작용
            inputActions.Player.Interactable.performed += OnInteractable;
            //상호작용
            inputActions.Player.Interactable.canceled += DisInteractable;
        }
        private void UseItem(IInteractable interactable)
        {
            if (interactable.IsDirectUse)
            {
                interactable.Use();
                useCheck.GetComponent<CapsuleCollider>().enabled = false;
            }
        }
        private void DisInteractable(InputAction.CallbackContext context)
        {
            useCheck.GetComponent<CapsuleCollider>().enabled = false;
        }
        private void OnInteractable(InputAction.CallbackContext context)
        {
            useCheck.GetComponent<CapsuleCollider>().enabled = true;
        }

        private void CharaterChangeButton_1(InputAction.CallbackContext context)
        {
            CurrentPickCharacterNum = 0;
        }
        private void CharaterChangeButton_2(InputAction.CallbackContext context)
        {
            CurrentPickCharacterNum = 1;
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

            if(movementInput != Vector2.zero && !isAttack && !isInAir)
            {
                playerCurrentStates = walkBool ? walkState : runState;
                playerCurrentStates.EnterState();
            }

            //if (movementInput != Vector2.zero && walkBool)
            //    walkState.EnterState();
            //else if (!walkBool && movementInput != Vector2.zero)
            //    runState.EnterState();
        }
        private void SprintButton(InputAction.CallbackContext _)
        {
            if(movementInput != Vector2.zero && !isAttack && !isInAir)
            {
                if(playerCurrentStates is AttackState)
                {
                    AttackState at = playerCurrentStates as AttackState;
                    at.ExitAttackState();
                }    
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

            if(!isAttack && !isInAir)
            {

                if (playerCurrentStates is AttackState)
                {
                    AttackState state = playerCurrentStates as AttackState;
                    state.ExitAttackState();
                }

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


        public void UseGravity(float gravity = -9.81f) //비행중 낙하
        {
            if (characterController.isGrounded == false)
            {
                moveDirection.y += gravity * Time.fixedDeltaTime;
            }
        }

        public void TestGravity()
        {
            moveDirection.y += gravity * Time.fixedDeltaTime;
        }

        public void InAirUseGravity(float gravity = -9.81f) //비행중 낙하
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


        //private void PlayerRotate()
        //{
        //    targetDirection = cameraObject.forward * moveDir.z;
        //    targetDirection = targetDirection + cameraObject.right * moveDir.x;
        //    targetDirection.Normalize();

        //    if (targetDirection == Vector3.zero)
        //        targetDirection = transform.forward;

        //    targetDirection.y = 0;


        //    Quaternion targerRotation = Quaternion.LookRotation(targetDirection);
        //    //Quaternion playerRoation = Quaternion.Slerp(transform.rotation, targerRotation, rotationSpeed * Time.fixedDeltaTime);

        //    transform.rotation = targerRotation;
        //    //transform.rotation = playerRoation;
        //}

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


        private void ChangeCharater(int pickCharacter)
        {
            currentPlayerCharater.gameObject.SetActive(false);
            currentPlayerCharater = pickChr[pickCharacter];
            currentPlayerCharater.gameObject.SetActive(true);

            //attackCollider = currentPlayerCharater.attackCollider;
            //현재 캐릭터의 오버라이드 애니메이터를 가져올 수 있다
            animator = currentPlayerCharater.GetComponent<Animator>();
            animator.runtimeAnimatorController = currentPlayerCharater.animator;

            AttackState at = attackState as AttackState;
            at.attackMove = currentPlayerCharater.Attack;
            at.ChangeAnimator(animator);
            
            playerCurrentStates.EnterState();
        }

        #region 애니메이션 이밴트
        //공격 애니메이션 정지 이동 외부에서 각 애니메이션에 부여
        //public void AttackMoveFlag()
        //{
        //    attackMove = attackMove ? false : true;
        //}
        //public void AttackColliderActive()
        //{

        //    attackCollider.enabled = attackCollider.enabled ? false : true;

        //}

        //public void AttackColliderDisable()
        //{
        //    attackCollider.enabled = false;
        //}

        public void ExitAttack()
        {
            attackCollider.enabled = false;
            handWeapon.SetActive(false);
            backWeapon.SetActive(true);
            //attackMove = false;

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

        #endregion
    }
}