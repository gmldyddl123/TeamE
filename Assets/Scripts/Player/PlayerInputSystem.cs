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
        SPRINT
    }
    public class PlayerInputSystem : MonoBehaviour
    {

        //���۳�Ʈ
        //Rigidbody playerRigidbody;
        PlayerInputAction inputActions;
        CharacterController characterController;
        Animator animator;


        //���� ����


        public PlayerState playerCurrentStates;
        PlayerState idleState;
        PlayerState walkState;
        PlayerState runState; 
        PlayerState sprintState;

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
        private float rotationSpeed = 7f;

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
            

            playerCurrentStates = idleState;
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
        }

        private void WalkButton(InputAction.CallbackContext _)
        {
            walkState.EnterState();
            walkBool = walkBool ? false : true;
        }

        private void SprintButton(InputAction.CallbackContext _)
        {
            sprintState.EnterState();
            walkBool = false;
        }

        private void MovementLogic(InputAction.CallbackContext context)
        {
            movementInput = context.ReadValue<Vector2>();
            moveDir.x = movementInput.x;
            moveDir.z = movementInput.y;


            if (movementInput == Vector2.zero)
            {
                idleState.EnterState();
            }
            else if(playerCurrentStates != sprintState && !walkBool)
            {
                runState.EnterState();
            }
            else if(walkBool)
            {
                walkState.EnterState();
            }
        }


        private void OnDisable()
        {
            inputActions.Player.Disable();
        }

        private void Update()
        {

        }
        private void FixedUpdate()
        {
            playerCurrentStates.MoveLogic();
        }


        public void PlayerMove(float moveSpeed)
        {
            if (characterController.isGrounded == false)
            {
                moveDirection.y += gravity * Time.fixedDeltaTime;
            }
            //else
            //{
            //    moveDirection.y = 0;
            //}
            characterController.Move(moveDirection * moveSpeed * Time.fixedDeltaTime);
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
            Quaternion playerRoation = Quaternion.Slerp(transform.rotation, targerRotation, rotationSpeed * Time.fixedDeltaTime);

            transform.rotation = playerRoation;
        }
    }

}