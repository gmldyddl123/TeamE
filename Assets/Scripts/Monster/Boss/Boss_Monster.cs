using player;
using System;
using UnityEngine;
using UnityEngine.AI;


namespace boss
{
    enum B_State
    {
        IDLE = 0,
        CHASE, //1
        ATTACK_1, //2
        ATTACK_2,//3
        SKILL_1,//4
        SKILL_2,//5
        SKILL_3,//6
        DIE,//7
        GROGGY//8
    }
    public class Boss_Monster : MonoBehaviour
    {
        /// <summary>
        /// 몬스터가 쫒는 목표의 Transform(플레이어)
        /// </summary>
        public Transform target { get; set; }                       
        /// <summary>
        /// 보스의 회전 속도
        /// </summary>
        public float rotationSpeed = 8;
        /// <summary>
        /// 보스의 콜라이더
        /// </summary>
        public CharacterController bossCollider;
        /// <summary>
        /// 플레이어
        /// </summary>
        public PlayerController player;
        /// <summary>
        /// 보스의 공격 사정거리
        /// </summary>
        public Boss_FOV_1 FOV1;
        /// <summary>
        /// 보스의 스킬 사정거리
        /// </summary>
        public Boss_FOV_2 FOV2;
        /// <summary>
        /// 몬스터를 통한 이벤트 모음집
        /// </summary>
        MonsterEvent monsterEvent;
        /// <summary>
        /// 보스의 네비메쉬
        /// </summary>
        public NavMeshAgent nav;
        /// <summary>
        /// 보스의 애니메이터
        /// </summary>
        public Animator animator;
        /// <summary>
        /// 보스의 공격이 쿨타임인지 아닌지에 대한 여부를 묻는 bool타입(true 일떄 공격 쿨타임 진행중)
        /// </summary>
        public bool isAtkCooldown { get; set; } = true;
        /// <summary>
        /// 공격중인지에 대한 여부를 묻는 bool타입 (true일떄 공격중)
        /// </summary>
        public bool isAttack { get; set; } = true;
       
        /// <summary>
        /// 보스의 스킬이 쿨타임인지 아닌지에 대한 여부를 묻는 bool타입(true 일떄 스킬 쿨타임 진행중)
        /// </summary>
        public bool isSkillCooldown = true;
        /// <summary>
        /// 보스 몬스터의 2페이즈 여부를 확인하는 bool타입(true = 현재 2페이즈)
        /// </summary>
        public bool Phaze_2 { get; set; } = false;
        /// <summary>
        /// 스킬을 사용중인지에 대한 여부를 묻는 bool타입 (true일떄 스킬 사용중)
        /// </summary>
        public bool isSkill { get; set; } = false;
        /// <summary>
        /// 2페이즈 애니메이션 재생 완료후 Skill_2 - MoveLogic을 한번만 진행시키기위한 조건 bool타입(애니메이션 이벤트 용 bool타입)
        /// </summary>
        public bool isPhaze2Success { get; set; } = false;
        /// <summary>
        /// 그로기 애니메이션 재생 완료후 Groggy - MoveLogic을 한번만 진행시키기위한 조건 bool타입(애니메이션 이벤트 용 bool타입)
        /// </summary>
        public bool isGroggySuccess { get; set; } = false;
        /// <summary>
        ///  Skill_3 의 데미지를 적용시킬때 사용하는 애니메이션 이벤트용 bool타입(true일때 스킬 범위 활성화)
        /// </summary>
        public bool isSkil_3_On { get; set; } = false;
        /// <summary>
        ///  Skill_1 의 데미지를 적용시킬때 사용하는 애니메이션 이벤트용 bool타입(true일때 스킬 범위 활성화)
        /// </summary>
        public bool isSkil_1_On { get; set; } = false;
        /// <summary>
        /// 스킬 사용 후 리셋할떄 사용하는 bool타입( false일떄 스킬 쿨타임 리셋) 
        /// </summary>
        public bool coolReset  = false;
        /// <summary>
        /// 그로기 진행중 그로기 카운트 감소를 막기위한 bool타입 ( true일때 그로기 수치 감소 x)
        /// </summary>
        public bool isGroggyCountChange = false;

        /// <summary>
        /// atk_1 동작에서 사용되는 무기 오브젝트
        /// </summary>
        public GameObject atk_1_Weapon;
        /// <summary>
        /// atk_2 동작에서 사용되는 무기 오브젝트
        /// </summary>
        public GameObject atk_2_Weapon;



        /// <summary>
        /// atk_1 파티클 이펙트를 가지고있는 오브젝트
        /// </summary>
        GameObject atk_1;
        /// <summary>
        /// atk_2 파티클 이펙트를 가지고있는 오브젝트
        /// </summary>
        GameObject atk_2;
        /// <summary>
        /// skill_1 파티클 이펙트를 가지고있는 오브젝트
        /// </summary>
        GameObject skill_1;
        /// <summary>
        /// skill_2 파티클 이펙트를 가지고있는 오브젝트
        /// </summary>
        GameObject skill_2;
        /// <summary>
        /// skill_3 파티클 이펙트를 가지고있는 오브젝트
        /// </summary>
        GameObject skill_3;

        readonly int AnimatorState = Animator.StringToHash("State");
        /// <summary>
        /// 업데이트 문에서 계속 변화하는 스킬쿨타임(현재 진행중인 스킬 쿨타임)
        /// </summary>
        public float skillCooldownTime = 0;
        /// <summary>
        /// 스킬을 사용하는는 빈도(스킬 쿨타임)
        /// </summary>
        public float skillCoolTime;

        /// <summary>
        /// 업데이트 문에서 계속 변화하는 공격 쿨타임(현재 진행 공격 쿨타임)
        /// </summary>
        public float atkCooldownTime = 0;
        /// <summary>
        /// 공격을 하는 빈도(공격 쿨타임)
        /// </summary>
        public float atkCoolTime;

        /// <summary>
        /// 현재 스테이트
        /// </summary>
        public MonsterState boss_CurrentStates;
        public MonsterState idleState;              
        public MonsterState chaseState;                                    
        public MonsterState attack_1_State;                                    
        public MonsterState attack_2_State;                                    
        public MonsterState skill_1_State;                                    
        public MonsterState skill_2_State;                                    
        public MonsterState skill_3_State;                                    
        public MonsterState dieState;                    
        public MonsterState groggyState;

        /// <summary>
        /// 페이즈 2 진입을 알리는 델리게이트
        /// </summary>
        public Action isPhaze2 { get; set; }

        /// <summary>
        /// 보스의 HP 변화를 알리는 델리게이트 UI 연동용
        /// </summary>
        public Action<float> bossHealthChange { get; set; }
        /// <summary>
        /// 보스의 Groggy게이지 변화를 알리는 델리게이트 UI 연동용
        /// </summary>
        public Action<float> bossGroggyChange { get; set; }

        
        public float MaxGroggy = 10;
        float groggy;
        public float Groggy
        {
            get => groggy;
            set
            {
                groggy = value;
                if (groggy <= 0 && (boss_CurrentStates != groggyState))
                { 
                    groggy = 0;
                    groggyState.EnterState();
                }
                groggy = Mathf.Clamp(groggy, 0, MaxGroggy);
                bossGroggyChange?.Invoke(groggy / MaxGroggy);
            }
        }
        
        public float MaxHP = 100;
        float hp = 100;
        public float HP
        {
            get => hp;
            set
            {
                hp = value;
                if (hp <= MaxHP * 0.5 && !Phaze_2) 
                {
                    Phaze_2 = true;
                    OnPhaze2();
                }
                else if(hp <= 0)
                {
                    hp = 0;
                    dieState.EnterState();
                }
                hp = Mathf.Clamp(hp, 0, MaxHP);
                bossHealthChange?.Invoke(hp / MaxHP);
            }
        }
        void Awake()
        {
            nav = GetComponent<NavMeshAgent>();
            FOV1 = GetComponentInChildren<Boss_FOV_1>();
            FOV2 = GetComponentInChildren<Boss_FOV_2>();
            bossCollider = GetComponent<CharacterController>();

            monsterEvent = FindObjectOfType<MonsterEvent>();

            Transform child = transform.GetChild(2).GetChild(0);
            atk_1 = child.gameObject;
            child = transform.GetChild(2).GetChild(1);
            atk_2 = child.gameObject;
            child = transform.GetChild(2).GetChild(3);
            skill_1 = child.gameObject;
            child = transform.GetChild(2).GetChild(4);
            skill_2 = child.gameObject;
            child = transform.GetChild(2).GetChild(5);
            skill_3 = child.gameObject;


            player = FindObjectOfType<PlayerController>();

            animator = GetComponent<Animator>();

            target = player.transform;

            isPhaze2 += OnPhaze2;

            idleState = new B_IdleState(this);
            chaseState = new B_ChaseState(this);
            attack_1_State = new B_Attack_1_State(this);
            attack_2_State = new B_Attack_2_State(this);
            skill_1_State = new B_Skill_1_State(this);
            skill_2_State = new B_Skill_2_State(this);
            skill_3_State = new B_Skill_3_State(this);
            dieState = new B_DieState(this);
            groggyState = new B_GroggyState(this);

            Groggy = MaxGroggy;
        }
        void Start()
        {
            idleState.EnterState();
        }

        /// <summary>
        /// 보스의 상태에 따라 애니메이션을 바꾸기위한 함수
        /// </summary>
        /// <param name="state">보스의 현재 스테이트</param>
        public void MonsterAnimatorChange(int state)
        {
            animator.SetInteger(AnimatorState, state);
        }
        /// <summary>
        /// 트리거로 발동하는 애니메이션을 위한 함수
        /// </summary>
        /// <param name="name">애니메이터 트리거 이름</param>
        public void MonsterTriggerChange(string name)
        { 
            animator.SetTrigger(name);
        }
       


        void Update()
        {
            if (isSkillCooldown)
            {
                skillCooldownTime += Time.deltaTime;

                if (skillCooldownTime >= skillCoolTime)
                {
                    isSkillCooldown = false;  
                    if(!coolReset)
                    {
                        skillCooldownTime = 0f;
                    }
                }
            }

            if (isAtkCooldown)
            {
                atkCooldownTime += Time.deltaTime;

                if (atkCooldownTime >= atkCoolTime)
                {
                    isAtkCooldown = false;
                    atkCooldownTime = 0f;
                }
            }
            boss_CurrentStates.MoveLogic();
        }

        /// <summary>
        /// 페이즈 2로 넘어가는 함수
        /// </summary>
        void OnPhaze2()
        {
            if(Phaze_2)
            {
                OnPhaze2();
                if(!isAttack)
                {
                    skill_2_State.EnterState();
                    Debug.Log("페이즈2시작");
                }
            }
        }

        /// <summary>
        /// 몬스터가 죽은후 Disable처리를 위한 함수
        /// </summary>
        public void AfterDie()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// 몬스터 사망시 발동하는 함수
        /// </summary>
        public void Die()
        {
            bossCollider.enabled = false;
            nav.enabled = false;
            monsterEvent.PlusQuestCount?.Invoke(1);
            monsterEvent.OnItemDrop?.Invoke();
        }



        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("PlayerAttackCollider"))
            {
                HP -= 4;
                if(!isGroggyCountChange)
                {
                    Groggy -= 1;
                }
                Debug.Log($"현재 HP : {HP}/{MaxHP}, 현재 그로기 게이지 : {Groggy}/ {MaxGroggy}");
                Debug.Log($"{boss_CurrentStates}");
            }
        }





//////////////////////// 보스의 애니메이션 이벤트용 함수 모음집 /////////////////////////////////////////////////

        public void Atk1_SwordEnable()
        {
            atk_1_Weapon.SetActive(true);
        }
        public void Atk2_SwordEnable()
        {
            atk_2_Weapon.SetActive(true);
        }
        public void Atk1_SwordDisable()
        {
            atk_1_Weapon.SetActive(false);
        }
        public void Atk2_SwordDisable()
        {
            atk_2_Weapon.SetActive(false);
        }

        public void Atk_1_OnEffect()
        {
            atk_1.SetActive(true);
        }
        public void Atk_2_OnEffect()
        {
            atk_2.SetActive(true);
        }

        public void Skill_1_OnEffect()
        {
            skill_1.SetActive(true);
        }
        public void Skill_2_OnEffect()
        {
            skill_2.SetActive(true);
        }

      
        public void Skill_3_OnEffect()
        {
            skill_3.SetActive(true);
        }

        public void phaze2Success()
        {
            isPhaze2Success = true;
        }
        public void groggySuccesss()
        {
            isGroggySuccess = true;
            animator.SetTrigger("GroggyFinish");
        }

        public void Skill_1_Hit_Start()
        {
            isSkil_1_On = true;
            isSkillCooldown = true;
        }
        public void Skill_1_Hit_Finish()
        {
            isSkil_1_On = false;
        }
        public void skillcool()
        {
            isSkillCooldown = true;
        }

        public void Skill_3_Hit_Start()
        {
            isSkil_3_On = true;
        }
        public void Skill_3_Hit_Finish()
        {
            isSkil_3_On = false;
        }

        public void everySkilloff()
        {
            atk_1.SetActive(false); 
            atk_2.SetActive(false);
            skill_1.SetActive(false);
            skill_3.SetActive(false);
        }

//////////////////////// 보스의 애니메이션 이벤트용 함수 모음집 /////////////////////////////////////////////////
    }
}


