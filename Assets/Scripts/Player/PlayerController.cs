using Cinemachine;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.EventSystems.EventTrigger;


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
        BowAim,      //10
        Hit
    }
    public partial class PlayerController : MonoBehaviour
    {
        //컴퍼넌트
        //Rigidbody playerRigidbody;
        PlayerInputAction inputActions;
        
        public CharacterController characterController;
        Animator animator;

        //현재 상태
        PlayerState playerCurrentStates;

        public PlayerState PlayerCurrentStates
        {
            get => playerCurrentStates;
            set
            {
                playerCurrentStates = value;


                if (playerCurrentStates == sprintState ||
                    playerCurrentStates == climbingState ||
                    playerCurrentStates == paraglidingState ||
                    playerCurrentStates == inAirState)
                {
                    //if(!staminaUI.activeSelf)
                    //{
                    //    staminaUI.SetActive(true);
                    //}
                    staminaUI.SetActive(true);
                    if (staminaRecoveryCoroutine != null)
                    {
                        StopCoroutine(staminaRecoveryCoroutine);
                        staminaRecoveryCoroutine = null;
                    }
                }
                else
                {
                    if (stamina < maxstamina && staminaRecoveryCoroutine == null && playerCurrentStates != inAirState)
                    {
                        staminaRecoveryCoroutine = StartCoroutine(StaminaRecovery());
                    }
                }
            }
        }

        //이 배열은 외부에서 접근하여 상태 엔터할려고 만듬
        PlayerState[] playerStates = new PlayerState[Enum.GetNames(typeof(State)).Length];
        //PlayerState[] playerStates = new PlayerState[11]; 

        PlayerState idleState;
        PlayerState walkState;
        PlayerState runState;
        PlayerState sprintState;
        //PlayerState jumpState; 점프는 InAir로 합병
        PlayerState inAirState;
        PlayerState paraglidingState;
        PlayerState slowDownState;
        PlayerState attackState;
        PlayerState skillState;
        PlayerState climbingState;
        PlayerState bowAimState;
        PlayerState hitState;

        //애니메이션
        //readonly int InputYString = Animator.StringToHash("InputY");
        readonly int AnimatorState = Animator.StringToHash("State");

        //걷기
        bool walkBool = false;

        //입력값
        private Vector2 movementInput; //액션으로 받는 입력값
        private Vector3 moveDir; //입력값으로 만든 벡터3

        public Vector3 MoveDir
        {
            get => moveDir;
            private set => moveDir = value;
        }

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

       // readonly int Attack_Hash = Animator.StringToHash("IsAttack");

        bool isAttack = false;
        public bool IsAttack
        {
            get => IsAttack;
            set
            {
                if(isAttack != value)
                {
                    isAttack = value;
                    //animator.SetBool(Attack_Hash, isAttack);
                }
            }
        }


        public float lockOnRange = 5.0f;
        Transform lockOnTarget;

        public Transform LockOnTarget
        {
            get => lockOnTarget;
            private set
            {
                if (lockOnTarget != value)  // 대상이 변경되었을 때만 실행
                {
                    lockOnTarget = value;

                    if (lockOnTarget != null)   // 락온 대상이 있으면
                    {
                        
                    }
                    else
                    {

                    }
                }
            }
        }


        public bool canAttack = true;

        //public bool attackMove { get; private set; } = false;

        //무기 소환

        //public GameObject handWeapon;
        //public GameObject backWeapon;

        public Action activeWeapon;
        public Action inactiveWeapon;


        /// <summary>
        /// 캐릭터 선택 폭
        /// </summary>
        //플레이어 스텟 각각의 공격과 무브 로직이 다르다
        public PlayerStat currentPlayerCharacter; // 현재 선택된
                                                 // 
        //CapsuleCollider attackCollider; //선택된 캐릭터의 공격 콜라이더 위치 바꿔줘야함 과거의 잔재임
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



        /// <summary>
        /// 보우 액션
        /// </summary>


        readonly int IsRangePlayer_Hash = Animator.StringToHash("IsRangePlayer");

        bool isAimCharecter = false;

        public bool IsAimCharecter
        {
            get => isAimCharecter;
            set
            {
                if(isAimCharecter != value)
                {
                    isAimCharecter = value;
                    animator.SetBool(IsRangePlayer_Hash, isAimCharecter);
                }
            }
        }

        public GameObject bowCrossHair;

        public CinemachineVirtualCamera aimCamera;

        //float aimCamera_Y = 1.15f;
        Vector2 aimCameraVector;
        float aimRoateSpeed = 5.0f;

        //허리
        Transform spine;
        public Vector3 relativeVec;

        //에임 최초 자리
        Vector3 REMEBER_BOW_AIM_VIEW_POINT = new(0.5f, 1.25f, 0.5f);
        
        //에임 위아래 막기
        //float aimRockTopDown = 1.05f;
        
        //허리 움직이는 커브
        public AnimationCurve aimLookCurve;

        //허리 움직이면서 따라오는 카메라 커브
        public AnimationCurve aimBackCameraCurve;

        //조준 위아래 제한
        float aimRockY_Max = 2.3f;
        float aimRockY_Min = 0.2f;

        //조준 민감도
        float bowAimSensitivy = 0.5f;

        //에임 카메라 움직이는 용도
        Transform bowAimViewPoint;
   

        bool bowAim = false;
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
                        activeWeapon?.Invoke();
                        bowCrossHair.SetActive(true);
                        aimCamera.Priority = 20;

                        bowAimState.EnterState();
                    }
                    else
                    {

                        RanagePlayer ra = currentPlayerCharacter as RanagePlayer;
                        ra.DrawBowString();
                        if(currentArrow!=null)
                        {
                            Destroy(currentArrow.gameObject);
                        }
                        inactiveWeapon?.Invoke();
                        bowCrossHair.SetActive(false);
                        aimCamera.Priority = 0;
                        if (!isHit)
                        {
                            //idleState.EnterState();
                            EnterDefalutGroundState();
                        }
                    }
                }
            }
        
        }

        //현재 손에 들고있는 화살 조준 모드를 해제할때 없애기 위한 용도
        //캐릭터 총괄 매니저가 없어서 퍼블릭으로 해야할듯
        public Player_Arrow currentArrow;
        
        //화살 발사시 활시위 돌아오는거에 사용됨
        Action fireArrow;



        /// <summary>
        /// 피격 관련
        /// </summary>

        //넉백 유무
        readonly int HitKnockback_Hash = Animator.StringToHash("HitKnockback");
        //피격 입장 트리거
        readonly int HitTrigger_Hash = Animator.StringToHash("HitTrigger");
        //피격도중 탈출가능할떄 키입력되면 즉시 탈출용도
        readonly int PressAnimExit_Hash = Animator.StringToHash("CanExitAnim");
        //사망 애니메이션 용도
        readonly int IsAlive_Hash = Animator.StringToHash("IsAlive");

        bool isHit = false;

        public bool IsHit
        {
            get => isHit;
            private set
            {
                animator.SetBool(PressAnimExit_Hash, false);
                if (value != isHit)
                {
                    isHit = value;
                    if (isHit)
                    {
                        BowAim = false;
                    }
                    //무적도 적용
                }
            }
        }

        bool knockback = false;
        public bool Knockback
        {
            get => knockback;
            set
            {
                if(value != knockback)
                {
                    knockback = value;
                    animator.SetBool(HitKnockback_Hash, knockback);
                }
            }
        }

        Vector3 attackHitPos;
        public Vector3 AttackHitPos { get => attackHitPos; }
        public Action<float, bool, Vector3> OnDamageAction;



        /// <summary>
        /// 스테미나 관련
        /// </summary>


        public GameObject staminaUI;

        float stamina = 1000.0f;

        public float Stamina
        {
            get => stamina;
            set
            {
                
                stamina = value;
                stamina = Mathf.Clamp(stamina, 0, Maxstamina);     // 스테미나는 항상 0~최대치

                onStaminaChange?.Invoke(stamina / Maxstamina);   // 스테미나 변화 알리기  
                if (stamina >= maxstamina)
                {
                    staminaUI.SetActive(false);
                }

            }
        }

        /// <summary>
        /// 최대 스테미나
        /// </summary>
        float maxstamina = 1000.0f;
        public float Maxstamina => maxstamina;

        /// <summary>
        /// 스테미나가 변경되었을 때 실행될 델리게이트
        /// </summary>
        public Action<float> onStaminaChange { get; set; }

        /// <summary>
        /// 캐릭터 변경시 해당 캐릭터로 체력바 변경
        /// </summary>
        public Action<PlayerStat> characterChangeHpBar;

        float staminaRecoveryPoint = 190.0f;


        public float minEnterStateStamina { get; } = 25.0f;



        Coroutine staminaRecoveryCoroutine;
        IEnumerator StaminaRecovery()
        {
            while(stamina < maxstamina)
            {
                Stamina += staminaRecoveryPoint * Time.deltaTime;



                yield return null;
            }
        }


        public void StaminaConsumption(float minuse)
        {
            Stamina -= minuse * Time.deltaTime;

            if(stamina <= 0)
            {
                if(playerCurrentStates == sprintState)
                {
                    runState.EnterState();
                }
                else if(playerCurrentStates == climbingState)
                {                   
                    lastMemorySpeed = 0;
                    moveDirection.y = 0;
                    isInAir = true;
                    inAirState.EnterState();
                }
                else
                {
                    isInAir = true;
                    moveDirection.y = 0;
                    inAirState.EnterState();

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
            currentPlayerCharacter = pickChr[0];
            //characterController = pickChr[0].GetComponent<CharacterController>();
            //attackCollider = currentPlayerCharater.attackCollider;
            //현재 캐릭터의 오버라이드 애니메이터를 가져올 수 있다
            animator = pickChr[0].GetComponent<Animator>();
            animator.runtimeAnimatorController = currentPlayerCharacter.animator;

            currentPlayerCharacter.SettingSummonWeapon();
            OnDamageAction = currentPlayerCharacter.OnDamage;

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
            skillState = new SkillState(this);
            climbingState = new ClimbingState(this, characterController, animator);
            bowAimState = new BowAimState(this, animator);
            hitState = new HitState(this, characterController);


            playerStates[0] = idleState;
            playerStates[1] = walkState;
            playerStates[2] = runState;
            playerStates[3] = sprintState;
            playerStates[4] = inAirState;
            playerStates[5] = paraglidingState;
            playerStates[6] = slowDownState;
            playerStates[7] = attackState;
            playerStates[8] = skillState;
            playerStates[9] = climbingState;
            playerStates[10] = bowAimState;
            playerStates[11] = hitState;

            if (attackState != null)
            {
                AttackState at = attackState as AttackState;
                at.attackMove = currentPlayerCharacter.AttackMove;
            }

            if(skillState != null)
            {
                SkillState st = skillState as SkillState;
                st.onSkillAction = currentPlayerCharacter.UltimateSkill;
            }
            //attackState. += playerStat.attackCollider;


            bowAimViewPoint = transform.Find("RangeAimViewPoint");
            //레이어 
            groundLayer = 1 << LayerMask.NameToLayer("Ground");

            //레이 충돌 체크
            mask = LayerMask.GetMask("Ground");


            playerCurrentStates = idleState;
            //playerCurrentStates = slowDownState;



            // 커서 락

           // Cursor.lockState = CursorLockMode.Locked;
           // Cursor.visible = false;

            //FindAnyObjectByType<UseChecker>().InputActionSetting(inputActions);


        }

        private void Start()
        {
            characterChangeHpBar?.Invoke(currentPlayerCharacter);
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

            inputActions.Player.SkillButton.performed += SkillButton;

            //캐릭터 변경
            inputActions.Player.CharacterChange_0.performed += CharaterChangeButton_0;
            inputActions.Player.CharacterChange_1.performed += CharaterChangeButton_1;


            //활 조준
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

                    aimCamera.transform.localPosition = new Vector3(
                     aimCamera.transform.localPosition.x,
                     aimBackCameraCurve.Evaluate(bowAimViewPoint.localPosition.y),
                     aimCamera.transform.localPosition.z
                     );
                }
                else if (aimCameraVector.y < -0.1f && bowAimViewPoint.localPosition.y >= aimRockY_Min)
                {
                    bowAimViewPoint.Translate(0, aimCameraVector.y * bowAimSensitivy * Time.deltaTime, 0);
                    //aimCamera.transform.Translate(0, (bowAimSensitivy -2)  * Time.deltaTime, 0, Space.World);

                    Mathf.Clamp(bowAimViewPoint.position.y, aimRockY_Min, aimRockY_Max);
                    //spine.rotation = spine.rotation * Quaternion.Euler(bowAimViewPoint.position);


                    //relativeVec.z = aimLookCurve.Evaluate(bowAimViewPoint.localPosition.y);
                    aimCamera.transform.localPosition = new Vector3(
                        aimCamera.transform.localPosition.x,
                        aimBackCameraCurve.Evaluate(bowAimViewPoint.localPosition.y),
                        aimCamera.transform.localPosition.z
                        );
                        
                        
                }

            }
            
        }

        private void AimMode(InputAction.CallbackContext context)
        {
            if(isAimCharecter && !isHit)
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
            if(!isHit)
            {
                Debug.Log(canAttack);
                if (bowAim)
                {
                    animator.SetTrigger("FireArrow");
                    fireArrow?.Invoke();
                    currentArrow.FireArrow();

                }
                else if (canAttack)
                {
                    AttackTargetLockOn();
                    if(LockOnTarget && moveDir == Vector3.zero)
                    {
                        moveDirection = (LockOnTarget.position - transform.position).normalized;
                        Vector3 rotVector = new Vector3(moveDirection.x, 0, moveDirection.z);
                        Quaternion targerRotation = Quaternion.LookRotation(rotVector);
                        transform.rotation = targerRotation;
                    }
                    else
                    {
                        LockOnTarget = null;
                        MoveToDir();
                    }
                    attackState.EnterState();
                }
            }
        }

        private void SkillButton(InputAction.CallbackContext _)
        {
            if(!isInAir && !isHit)
            {
                skillState.EnterState();
                activeWeapon?.Invoke();
            }
        }


        private void JumpButton(InputAction.CallbackContext _)
        {
            if (isHit)
                return;

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
                    if(stamina > minEnterStateStamina)
                    {
                        if (!Physics.Raycast(transform.position, Vector3.down, characterController.height * 1.5f, groundLayer))
                        {
                            isParagliding = true;
                            paraglidingState.EnterState();

                        }
                    }   
                }
            }
        }

        private void WalkButton(InputAction.CallbackContext _)
        {
            walkBool = walkBool ? false : true;

            if(movementInput != Vector2.zero && !isAttack && !isInAir && !isHit)
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
            if(movementInput != Vector2.zero && stamina > minEnterStateStamina && !isInAir && !isHit)
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


            if (isHit) return;

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
                    Debug.Log("공중상태에 들어옴");
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

            if (!bowAim && playerCurrentStates != slowDownState && stamina > minEnterStateStamina)
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

        public void UseGravity(float gravity = -9.81f) //비행중 낙하
        {
            if (characterController.isGrounded == false)
            {
                if (moveDirection.y > -10f)
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

        public void PlayerEnterInAirClimbingState()
        {
            isInAir = false;
            climbingState.EnterState();
        }

        public void PlayerAnimoatorChage(int state)
        {
            animator.SetInteger(AnimatorState, state);
            if(state == 11)
            {
                IsHit = true;
                animator.SetTrigger(HitTrigger_Hash);
            }
        }

        public void PlayerDieAnimatorParamater(bool isAlive)
        {
            animator.SetBool(IsAlive_Hash, isAlive);
        }

        public void DieToAliveCharacterChange()
        {
            for(int i = 0; i < pickChr.Length; i++)
            {
                if (pickChr[i].IsAlive)
                {
                    ChangeCharater(i);
                    PlayerDieAnimatorParamater(currentPlayerCharacter.IsAlive);
                    break;
                }
            }
        }


        public void EnterDefalutGroundState()
        {
            if(IsHit)
            {
                IsHit = false;
                animator.SetBool(PressAnimExit_Hash, true);
            }
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


        public void ControlEnterState(int state)
        {
            if(state == 11)
            {
                animator.SetBool(PressAnimExit_Hash, false);
            }
            playerStates[state].EnterState();
        }
        public void ControlEnterState(int state, bool knockback, Vector3 attackPos)
        {
            Knockback = knockback;
            attackHitPos = attackPos;

            transform.LookAt(new Vector3(
                attackHitPos.x,
                transform.position.y,
                attackHitPos.z));

            ControlEnterState(state);
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


                if(playerCurrentStates != bowAimState)
                {
                    //회전
                    Quaternion targerRotation = Quaternion.LookRotation(cameraAngleCalculation);
                    transform.rotation = targerRotation;
                }

                //스무스 회전
                //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                //transform.rotation = Quaternion.Euler(0.0f, angle, 0f);
            }

            //PlayerRotate();
        }



        void AttackTargetLockOn()
        {
            // 주변에 적이 있는지 확인
            Collider[] enemies = Physics.OverlapSphere(transform.position, lockOnRange, LayerMask.GetMask("AttackTarget"));
            if (enemies.Length > 0)
            {
                // 적이 있으면
                // 가장 가까운 적 찾기
                Transform nearest = null;
                float nearestDistance = float.MaxValue;
                foreach (var enemy in enemies)
                {
                    Vector3 dir = enemy.transform.position - transform.position;    // 방향 벡터 구하고
                    float distanceSqr = dir.sqrMagnitude;                           // 방향 벡터의 길이 확인(= 거리 비교)
                    if (distanceSqr < nearestDistance)
                    {
                        nearestDistance = distanceSqr;      // 가장 가까운 것 구하기
                        nearest = enemy.transform;
                    }
                }

                LockOnTarget = nearest;         // 가장 가까운 적을 LockOnTarget으로 설정
            }
            else
            {
                LockOnTarget = null;            // 주변에 적이 없으면 LockOnTarget 비우기
            }
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
            currentPlayerCharacter.gameObject.SetActive(false);
            currentPlayerCharacter = pickChr[pickCharacter];
            currentPlayerCharacter.gameObject.SetActive(true);


            currentPlayerCharacter.SettingSummonWeapon();

            OnDamageAction = currentPlayerCharacter.OnDamage;

            //attackCollider = currentPlayerCharater.attackCollider;
            //현재 캐릭터의 오버라이드 애니메이터를 가져올 수 있다
            animator = currentPlayerCharacter.GetComponent<Animator>();
            animator.runtimeAnimatorController = currentPlayerCharacter.animator;

            RanagePlayer rn = currentPlayerCharacter.GetComponent<RanagePlayer>();
            if (rn != null)
            {

                IsAimCharecter = true;
                spine = animator.GetBoneTransform(HumanBodyBones.Spine);
                inputActions.Player.CameraLook.performed += AimCameraRotate;
                BowAimState bo = bowAimState as BowAimState;

                //델리게이트 연결
                fireArrow = rn.FireArrow;
                

                bo.ChangeAnimator(animator);


            }
            else
            {
                IsAimCharecter = false;
                if(BowAim)
                {
                    BowAim = false;
                    inputActions.Player.CameraLook.performed -= AimCameraRotate;
                }
            }

            

            AttackState at = attackState as AttackState;
            at.attackMove = currentPlayerCharacter.AttackMove;
            at.ChangeAnimator(animator);

            ClimbingState cl = climbingState as ClimbingState;
            cl.ChangeAnimator(animator);

            //BowAimState bo = bowAimState as BowAimState;
            //bo.ChangeAnimator(animator);
            characterChangeHpBar?.Invoke(currentPlayerCharacter);

            playerCurrentStates.EnterState();
        }



        public void StopInputKey(bool turnOn)
        {
            if(turnOn)
            {
                inputActions.Player.Enable();
            }
            else
            {
                inputActions.Player.Disable();

            }
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