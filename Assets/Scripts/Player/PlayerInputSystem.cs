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
        SlowDown
    }
    public class PlayerInputSystem : MonoBehaviour
    {
        
        //���۳�Ʈ
        //Rigidbody playerRigidbody;
        PlayerInputAction inputActions;
        public CharacterController characterController;
        Animator animator;


        //���� ����
        public PlayerState playerCurrentStates;
        PlayerState idleState;
        PlayerState walkState;
        PlayerState runState; 
        PlayerState sprintState;
        //PlayerState jumpState;
        PlayerState inAirState;
        PlayerState paraglidingState;
        PlayerState slowDownState ;

        //�ִϸ��̼�
        //readonly int InputYString = Animator.StringToHash("InputY");
        readonly int AnimatorState = Animator.StringToHash("State");
        bool walkBool = false;

        //�Է°�
        private Vector2 movementInput; //�׼����� �޴� �Է°�
        private Vector3 moveDir; //�Է°����� ���� ����3

        //ĳ���� ��Ʈ�ѷ�
        float gravity = -9.81f; // �߷�
        public Vector3 moveDirection; // ī�޶���� ����� �̵� ����

        //ȸ��
        Transform cameraObject;
        Vector3 targetDirection = Vector3.zero; //ȸ���ϴ� ����

        //���� ����
        public float lastMemorySpeed = 0.0f;
        bool isInAir = false;

        int groundLayer;
        bool fallingDirYSetComplete = false;

        //�з� �۶��̵�

        bool isParagliding = false;
        private float rotationSpeed = 2f;



        private void Awake()
        {
            //playerRigidbody = GetComponent<Rigidbody>();
            inputActions = new PlayerInputAction();
            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            cameraObject = Camera.main.transform;


            //����
            idleState = new IdleState(this);
            walkState = new WalkState(this);
            runState = new RunState(this);
            sprintState = new SprintState(this);
            //jumpState = new JumpState(this, characterController);
            inAirState = new InAirState(this, characterController);
            paraglidingState = new ParaglidingState(this, characterController);
            slowDownState = new SlowDownState(this, animator,characterController);

            //���̾� 
            groundLayer = 1 << LayerMask.NameToLayer("Ground");

            //playerCurrentStates = idleState;
            playerCurrentStates = slowDownState;
            // Ŀ�� ��
            //Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnEnable()
        {

            //��ǲ�ý���
            inputActions.Player.Enable();
            
            //WASD
            inputActions.Player.Movement.performed += MovementLogic;
            inputActions.Player.Movement.canceled += MovementLogic;

            //Shift ���� ����
            inputActions.Player.Sprint.performed += SprintButton;
            //inputActions.Player.CameraLook.performed += i => cameraInput = i.ReadValue<Vector2>();

            //Control �ȱ�
            inputActions.Player.Walk.performed += WalkButton;

            //Space ����
            inputActions.Player.Jump.performed += JumpButton;

        }

        private void JumpButton(InputAction.CallbackContext _)
        {
            //if (characterController.isGrounded == true)
            //{
            //    //jumpState.EnterState();
               
            //}

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
            if(walkBool)
                walkState.EnterState();
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
                    Debug.Log("����?");
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

        private void FixedUpdate()
        {
           // Debug.Log(transform.position.y);
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
            playerCurrentStates = idleState;
        }
        public void PlayerAnimoatrChage(int state)
        {
            animator.SetInteger(AnimatorState, state);
        }

        public void MoveToDir()
        {
            Vector3 movedis = cameraObject.rotation * new Vector3(moveDir.x, 0, moveDir.z);

            moveDirection = new Vector3(movedis.x, moveDirection.y, movedis.z);
            PlayerRotate();
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

        public void PlayerRotateSlerp()//�з� �۶���̿� ����� 
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