using Cinemachine;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using Unity.VisualScripting;
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
        Paragliding,    //5
        SlowDown,
        Attack,
        Skill,
        Climbing,
        BowAim      //10
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
        PlayerState bowAimState;

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



        /// <summary>
        /// ���� �׼�
        /// </summary>


        bool isAimCharecter = false;
        bool bowAim = false;

        public GameObject bowCrossHair;

        public CinemachineVirtualCamera aimCamera;

        //float aimCamera_Y = 1.15f;
        Vector2 aimCameraVector;
        float aimRoateSpeed = 5.0f;

        //�㸮
        Transform spine;
        public Vector3 relativeVec;

        //���� ���� �ڸ�
        Vector3 REMEBER_BOW_AIM_VIEW_POINT = new(0.5f, 1.25f, 0.5f);
        
        //���� ���Ʒ� ����
        //float aimRockTopDown = 1.05f;
        
        //�㸮 �����̴� Ŀ��
        public AnimationCurve aimLookCurve;

        public AnimationCurve aimBackCameraCurve;


        float aimRockY_Max = 2.3f;
        float aimRockY_Min = 0.2f;

        float bowAimSensitivy = 0.5f;

        //���� ī�޶� �����̴� �뵵
        Transform bowAimViewPoint;
   

        bool BowAim
        {
            get => bowAim;
            set
            {
                if(value != bowAim)
                {
                    bowAim = value;

                    if(bowAim)
                    {
                        bowAimViewPoint.localPosition = REMEBER_BOW_AIM_VIEW_POINT;
                        bowCrossHair.SetActive(true);
                        aimCamera.Priority = 20;

                        bowAimState.EnterState();
                    }
                    else
                    {

                        RanagePlayer ra = currentPlayerCharater as RanagePlayer;
                        ra.DrawBowString();

                        bowCrossHair.SetActive(false);
                        aimCamera.Priority = 0;
                    }
                }
            }
        
        }


        Action fireArrow;


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
            bowAimState = new BowAimState(this, animator);

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


            bowAimViewPoint = transform.Find("RangeAimViewPoint");
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


            //Ȱ ����
            inputActions.Player.BowAim.performed += AimMode;
            //inputActions.Player.CameraLook.performed += AimCameraRotate;



        }

        private void AimCameraRotate(InputAction.CallbackContext context)
        {

            if(BowAim)
            {                
                aimCameraVector = context.ReadValue<Vector2>();

                if (aimCameraVector.x > 0.1f)
                {
                    transform.Rotate(0, aimCameraVector.x * aimRoateSpeed * Time.deltaTime, 0);
                }
                else if (aimCameraVector.x < -0.1f)
                {
                    transform.Rotate(0, aimCameraVector.x * aimRoateSpeed * Time.deltaTime, 0);
                }

                if (aimCameraVector.y > 0.1f && bowAimViewPoint.localPosition.y <= aimRockY_Max)
                {
                    bowAimViewPoint.Translate(0, aimCameraVector.y * bowAimSensitivy * Time.deltaTime, 0);
                    //aimCamera.transform.Translate(0, -(bowAimSensitivy-2) * Time.deltaTime, 0, Space.World);

                    Mathf.Clamp(bowAimViewPoint.position.y, aimRockY_Min, aimRockY_Max);
                    //spine.rotation = spine.rotation * Quaternion.Euler(bowAimViewPoint.position);

                    aimCamera.transform.position = new Vector3(
                     aimCamera.transform.position.x,
                     aimBackCameraCurve.Evaluate(bowAimViewPoint.localPosition.y),
                     aimCamera.transform.position.z
                     );
                }
                else if (aimCameraVector.y < -0.1f && bowAimViewPoint.localPosition.y >= aimRockY_Min)
                {
                    bowAimViewPoint.Translate(0, aimCameraVector.y * bowAimSensitivy * Time.deltaTime, 0);
                    //aimCamera.transform.Translate(0, (bowAimSensitivy -2)  * Time.deltaTime, 0, Space.World);

                    Mathf.Clamp(bowAimViewPoint.position.y, aimRockY_Min, aimRockY_Max);
                    //spine.rotation = spine.rotation * Quaternion.Euler(bowAimViewPoint.position);


                    //relativeVec.z = aimLookCurve.Evaluate(bowAimViewPoint.localPosition.y);
                    aimCamera.transform.position = new Vector3(
                        aimCamera.transform.position.x,
                        aimBackCameraCurve.Evaluate(bowAimViewPoint.localPosition.y),
                        aimCamera.transform.position.z
                        );
                        
                        
                }

            }
            
        }

        private void AimMode(InputAction.CallbackContext context)
        {
            if(isAimCharecter)
            {
                BowAim = !bowAim;
            }
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
            if(bowAim)
            {
                animator.SetTrigger("FireArrow");

                Debug.Log(fireArrow);
                fireArrow?.Invoke();
                
            }
            else if(canAttack)
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


            if (playerCurrentStates == climbingState)
            {
                if(exitWallState)
                {
                    exitWallState = false;
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
            else if (!isAttack && !isInAir && !bowAim)
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

        private void LateUpdate()
        {
            if(bowAim)
            {
                //Vector3 targetPos = new Vector3(
                //    spine.position.x,
                //    spine.position.y - 90,
                //    bowAimViewPoint.position.z - 90);
                //spine.LookAt(targetPos);

                // float lerpAimPoint = Mathf.Lerp(aimRockY_Min, aimRockY_Max, bowAimViewPoint.localPosition.y);

                //float lerpAimPoint = Mathf.Clamp(bowAimViewPoint.localPosition.y, aimRockY_Min, aimRockY_Max);
                //Mathf.Clamp01(lerpAimPoint);

                //Debug.Log(Mathf.Clamp01(lerpAimPoint));

                //Debug.Log(lerpAimPoint);
                //if(lerpAimPoint < 0.5f)
                //{
                //    relativeVec.z = bowAimViewPoint.localPosition.y * -20.0f;
                //}
                //else
                //{
                //    relativeVec.z = bowAimViewPoint.localPosition.y * 20.0f;
                //}

                relativeVec.z = aimLookCurve.Evaluate(bowAimViewPoint.localPosition.y);
                spine.rotation = spine.rotation * Quaternion.Euler(relativeVec);

            }
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

            if (!bowAim && playerCurrentStates != slowDownState)
            {
                CheckFrontWall();
                if (isWallHit)
                {
                    climbingState.EnterState();
                }
                //else
                //{
                //    characterController.Move(moveDirection * moveSpeed * Time.fixedDeltaTime);
                    
                    
                //}
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

        public void PlayerEnterInAirClimbingState()
        {
            isInAir = false;
            climbingState.EnterState();
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


                if(playerCurrentStates != bowAimState)
                {
                    //ȸ��
                    Quaternion targerRotation = Quaternion.LookRotation(cameraAngleCalculation);
                    transform.rotation = targerRotation;
                }

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

            RanagePlayer rn = currentPlayerCharater.GetComponent<RanagePlayer>();
            Debug.Log(rn);
            if (rn != null)
            {
                
                isAimCharecter = true;
                spine = animator.GetBoneTransform(HumanBodyBones.Spine);
                inputActions.Player.CameraLook.performed += AimCameraRotate;
                BowAimState bo = bowAimState as BowAimState;

                //��������Ʈ ����
                fireArrow = rn.FireArrow;
                

                bo.ChangeAnimator(animator);


            }
            else
            {
                isAimCharecter = false;
                if(BowAim)
                {
                    BowAim = false;
                    inputActions.Player.CameraLook.performed -= AimCameraRotate;
                }
            }

            

            AttackState at = attackState as AttackState;
            at.attackMove = currentPlayerCharater.AttackMove;
            at.ChangeAnimator(animator);

            ClimbingState cl = climbingState as ClimbingState;
            cl.ChangeAnimator(animator);

            //BowAimState bo = bowAimState as BowAimState;
            //bo.ChangeAnimator(animator);


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