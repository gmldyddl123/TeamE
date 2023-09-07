using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        Attack,
        Skill,
        Climbing
    }
    public partial class PlayerController : MonoBehaviour
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
        //PlayerState jumpState; ������ InAir�� �պ�
        PlayerState inAirState;
        PlayerState paraglidingState;
        PlayerState slowDownState;
        PlayerState attackState;
        PlayerState skillState;
        PlayerState climbingState;

        //�ִϸ��̼�
        //readonly int InputYString = Animator.StringToHash("InputY");
        readonly int AnimatorState = Animator.StringToHash("State");

        //�ȱ�
        bool walkBool = false;

        //�Է°�
        private Vector2 movementInput; //�׼����� �޴� �Է°�
        private Vector3 moveDir; //�Է°����� ���� ����3

        public Vector3 MoveDir 
        { 
            get => moveDir; 
            private set => moveDir = value;
        }

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

        //����
        public bool isAttack { get; set; } = false;
        
        public bool canAttack = true;

        //public bool attackMove { get; private set; } = false;

        //���� ��ȯ

        public GameObject handWeapon;
        public GameObject backWeapon;

        /// <summary>
        /// ĳ���� ���� ��
        /// </summary>
        //�÷��̾� ���� ������ ���ݰ� ���� ������ �ٸ���
        public PlayerStat currentPlayerCharater; // ���� ���õ� ĳ����
        //CapsuleCollider attackCollider; //���õ� ĳ������ ���� �ݶ��̴� ��ġ �ٲ������ ������ ������
        const int maxPickCharacter = 2; // �ִ� ���� ĳ����
        public PlayerStat[] pickChr = new PlayerStat[maxPickCharacter]; //�׽�Ʈ�� �ۺ� �� ���ִ� ĳ���͵�

        int currentPickCharacterNum = 0; //ĳ���� ������ ���Ѱ� 1�� 2�� ������ �� ���ڷ� ����
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

            //������ ĳ���� ���� �ҷ�����
            currentPlayerCharater = pickChr[0];
            //characterController = pickChr[0].GetComponent<CharacterController>();
            //attackCollider = currentPlayerCharater.attackCollider;
            //���� ĳ������ �������̵� �ִϸ����͸� ������ �� �ִ�
            animator = pickChr[0].GetComponent<Animator>();
            animator.runtimeAnimatorController = currentPlayerCharater.animator;


            //����
            idleState = new IdleState(this);
            walkState = new WalkState(this);
            runState = new RunState(this);
            sprintState = new SprintState(this);
            //jumpState = new JumpState(this, characterController);
            inAirState = new InAirState(this, characterController);
            paraglidingState = new ParaglidingState(this, characterController);
            slowDownState = new SlowDownState(this);
            attackState = new AttackState(this, animator);
            skillState = new SkillState(this);
            climbingState = new ClimbingState(this, characterController, animator);

            if (attackState != null)
            {
                AttackState at = attackState as AttackState;
                at.attackMove = currentPlayerCharater.AttackMove;
            }

            if(skillState != null)
            {
                SkillState st = skillState as SkillState;
                st.onSkillAction = currentPlayerCharater.UltimateSkill;
            }
            //attackState. += playerStat.attackCollider;

            //���̾� 
            groundLayer = 1 << LayerMask.NameToLayer("Ground");

            //���� �浹 üũ
            mask = LayerMask.GetMask("Ground");


            playerCurrentStates = idleState;
            //playerCurrentStates = slowDownState;
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

            //���콺 ��Ŭ�� ����
            inputActions.Player.Attack.performed += AttackButton;

            inputActions.Player.SkillButton.performed += SkillButton;

            //ĳ���� ����
            inputActions.Player.CharacterChange_0.performed += CharaterChangeButton_0;
            inputActions.Player.CharacterChange_1.performed += CharaterChangeButton_1;


        }


        private void CharaterChangeButton_0(InputAction.CallbackContext _)
        {
            CurrentPickCharacterNum = 0;
        }
        private void CharaterChangeButton_1(InputAction.CallbackContext _)
        {
            CurrentPickCharacterNum = 1;
        }

        private void AttackButton(InputAction.CallbackContext _)
        {
            if(canAttack)
            {
                attackState.EnterState();
            }
        }

        private void SkillButton(InputAction.CallbackContext _)
        {
            if(!isInAir)
                skillState.EnterState();
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

            Debug.Log(moveDir);

            if (playerCurrentStates == climbingState)
            {

            }
            else if (!isAttack && !isInAir)
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

            //Debug.Log(moveDirection);
            CheckFrontWall();
            if (!isWallHit)
            {
                characterController.Move(moveDirection * moveSpeed * Time.fixedDeltaTime);
            }
            else
            {
                climbingState.EnterState();
                //Debug.Log("���� ����");
            }
        }

        public void UseGravity(float gravity = -9.81f) //������ ����
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

        public void InAirUseGravity(float gravity = -9.81f) //������ ����
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

                //�̵� ����
                Vector3 cameraAngleCalculation = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                moveDirection = new Vector3(cameraAngleCalculation.x, moveDirection.y, cameraAngleCalculation.z);
                moveDirection.Normalize();

                //ȸ��
                Quaternion targerRotation = Quaternion.LookRotation(cameraAngleCalculation);
                transform.rotation = targerRotation;

                //������ ȸ��
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


        private void ChangeCharater(int pickCharacter)
        {
            currentPlayerCharater.gameObject.SetActive(false);
            currentPlayerCharater = pickChr[pickCharacter];
            currentPlayerCharater.gameObject.SetActive(true);

            //attackCollider = currentPlayerCharater.attackCollider;
            //���� ĳ������ �������̵� �ִϸ����͸� ������ �� �ִ�
            animator = currentPlayerCharater.GetComponent<Animator>();
            animator.runtimeAnimatorController = currentPlayerCharater.animator;

            AttackState at = attackState as AttackState;
            at.attackMove = currentPlayerCharater.AttackMove;
            at.ChangeAnimator(animator);
            
            playerCurrentStates.EnterState();
        }


       
        #region �ִϸ��̼� �̹�Ʈ
        //���� �ִϸ��̼� ���� �̵� �ܺο��� �� �ִϸ��̼ǿ� �ο�
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

        //public void ExitAttack()
        //{
        //    attackCollider.enabled = false;
        //    handWeapon.SetActive(false);
        //    backWeapon.SetActive(true);
        //    //attackMove = false;

        //    MoveToDir();
        //    if (movementInput == Vector2.zero)
        //    {
        //        //slowDownState.EnterState();
        //        idleState.EnterState();
        //    }
        //    else if (playerCurrentStates != sprintState && !walkBool)
        //    {
        //        runState.EnterState();
        //    }
        //    else if (walkBool)
        //    {
        //        walkState.EnterState();
        //    }
        //}

        #endregion
    }
}