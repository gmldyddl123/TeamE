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
        /// ���Ͱ� �i�� ��ǥ�� Transform(�÷��̾�)
        /// </summary>
        public Transform target { get; set; }                       
        /// <summary>
        /// ������ ȸ�� �ӵ�
        /// </summary>
        public float rotationSpeed = 8;
        /// <summary>
        /// ������ �ݶ��̴�
        /// </summary>
        public CharacterController bossCollider;
        /// <summary>
        /// �÷��̾�
        /// </summary>
        public PlayerController player;
        /// <summary>
        /// ������ ���� �����Ÿ�
        /// </summary>
        public Boss_FOV_1 FOV1;
        /// <summary>
        /// ������ ��ų �����Ÿ�
        /// </summary>
        public Boss_FOV_2 FOV2;
        /// <summary>
        /// ���͸� ���� �̺�Ʈ ������
        /// </summary>
        MonsterEvent monsterEvent;
        /// <summary>
        /// ������ �׺�޽�
        /// </summary>
        public NavMeshAgent nav;
        /// <summary>
        /// ������ �ִϸ�����
        /// </summary>
        public Animator animator;
        /// <summary>
        /// ������ ������ ��Ÿ������ �ƴ����� ���� ���θ� ���� boolŸ��(true �ϋ� ���� ��Ÿ�� ������)
        /// </summary>
        public bool isAtkCooldown { get; set; } = true;
        /// <summary>
        /// ������������ ���� ���θ� ���� boolŸ�� (true�ϋ� ������)
        /// </summary>
        public bool isAttack { get; set; } = true;
       
        /// <summary>
        /// ������ ��ų�� ��Ÿ������ �ƴ����� ���� ���θ� ���� boolŸ��(true �ϋ� ��ų ��Ÿ�� ������)
        /// </summary>
        public bool isSkillCooldown = true;
        /// <summary>
        /// ���� ������ 2������ ���θ� Ȯ���ϴ� boolŸ��(true = ���� 2������)
        /// </summary>
        public bool Phaze_2 { get; set; } = false;
        /// <summary>
        /// ��ų�� ����������� ���� ���θ� ���� boolŸ�� (true�ϋ� ��ų �����)
        /// </summary>
        public bool isSkill { get; set; } = false;
        /// <summary>
        /// 2������ �ִϸ��̼� ��� �Ϸ��� Skill_2 - MoveLogic�� �ѹ��� �����Ű������ ���� boolŸ��(�ִϸ��̼� �̺�Ʈ �� boolŸ��)
        /// </summary>
        public bool isPhaze2Success { get; set; } = false;
        /// <summary>
        /// �׷α� �ִϸ��̼� ��� �Ϸ��� Groggy - MoveLogic�� �ѹ��� �����Ű������ ���� boolŸ��(�ִϸ��̼� �̺�Ʈ �� boolŸ��)
        /// </summary>
        public bool isGroggySuccess { get; set; } = false;
        /// <summary>
        ///  Skill_3 �� �������� �����ų�� ����ϴ� �ִϸ��̼� �̺�Ʈ�� boolŸ��(true�϶� ��ų ���� Ȱ��ȭ)
        /// </summary>
        public bool isSkil_3_On { get; set; } = false;
        /// <summary>
        ///  Skill_1 �� �������� �����ų�� ����ϴ� �ִϸ��̼� �̺�Ʈ�� boolŸ��(true�϶� ��ų ���� Ȱ��ȭ)
        /// </summary>
        public bool isSkil_1_On { get; set; } = false;
        /// <summary>
        /// ��ų ��� �� �����ҋ� ����ϴ� boolŸ��( false�ϋ� ��ų ��Ÿ�� ����) 
        /// </summary>
        public bool coolReset  = false;
        /// <summary>
        /// �׷α� ������ �׷α� ī��Ʈ ���Ҹ� �������� boolŸ�� ( true�϶� �׷α� ��ġ ���� x)
        /// </summary>
        public bool isGroggyCountChange = false;

        /// <summary>
        /// atk_1 ���ۿ��� ���Ǵ� ���� ������Ʈ
        /// </summary>
        public GameObject atk_1_Weapon;
        /// <summary>
        /// atk_2 ���ۿ��� ���Ǵ� ���� ������Ʈ
        /// </summary>
        public GameObject atk_2_Weapon;



        /// <summary>
        /// atk_1 ��ƼŬ ����Ʈ�� �������ִ� ������Ʈ
        /// </summary>
        GameObject atk_1;
        /// <summary>
        /// atk_2 ��ƼŬ ����Ʈ�� �������ִ� ������Ʈ
        /// </summary>
        GameObject atk_2;
        /// <summary>
        /// skill_1 ��ƼŬ ����Ʈ�� �������ִ� ������Ʈ
        /// </summary>
        GameObject skill_1;
        /// <summary>
        /// skill_2 ��ƼŬ ����Ʈ�� �������ִ� ������Ʈ
        /// </summary>
        GameObject skill_2;
        /// <summary>
        /// skill_3 ��ƼŬ ����Ʈ�� �������ִ� ������Ʈ
        /// </summary>
        GameObject skill_3;

        readonly int AnimatorState = Animator.StringToHash("State");
        /// <summary>
        /// ������Ʈ ������ ��� ��ȭ�ϴ� ��ų��Ÿ��(���� �������� ��ų ��Ÿ��)
        /// </summary>
        public float skillCooldownTime = 0;
        /// <summary>
        /// ��ų�� ����ϴ´� ��(��ų ��Ÿ��)
        /// </summary>
        public float skillCoolTime;

        /// <summary>
        /// ������Ʈ ������ ��� ��ȭ�ϴ� ���� ��Ÿ��(���� ���� ���� ��Ÿ��)
        /// </summary>
        public float atkCooldownTime = 0;
        /// <summary>
        /// ������ �ϴ� ��(���� ��Ÿ��)
        /// </summary>
        public float atkCoolTime;

        /// <summary>
        /// ���� ������Ʈ
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
        /// ������ 2 ������ �˸��� ��������Ʈ
        /// </summary>
        public Action isPhaze2 { get; set; }

        /// <summary>
        /// ������ HP ��ȭ�� �˸��� ��������Ʈ UI ������
        /// </summary>
        public Action<float> bossHealthChange { get; set; }
        /// <summary>
        /// ������ Groggy������ ��ȭ�� �˸��� ��������Ʈ UI ������
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
        /// ������ ���¿� ���� �ִϸ��̼��� �ٲٱ����� �Լ�
        /// </summary>
        /// <param name="state">������ ���� ������Ʈ</param>
        public void MonsterAnimatorChange(int state)
        {
            animator.SetInteger(AnimatorState, state);
        }
        /// <summary>
        /// Ʈ���ŷ� �ߵ��ϴ� �ִϸ��̼��� ���� �Լ�
        /// </summary>
        /// <param name="name">�ִϸ����� Ʈ���� �̸�</param>
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
        /// ������ 2�� �Ѿ�� �Լ�
        /// </summary>
        void OnPhaze2()
        {
            if(Phaze_2)
            {
                OnPhaze2();
                if(!isAttack)
                {
                    skill_2_State.EnterState();
                    Debug.Log("������2����");
                }
            }
        }

        /// <summary>
        /// ���Ͱ� ������ Disableó���� ���� �Լ�
        /// </summary>
        public void AfterDie()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// ���� ����� �ߵ��ϴ� �Լ�
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
                Debug.Log($"���� HP : {HP}/{MaxHP}, ���� �׷α� ������ : {Groggy}/ {MaxGroggy}");
                Debug.Log($"{boss_CurrentStates}");
            }
        }





//////////////////////// ������ �ִϸ��̼� �̺�Ʈ�� �Լ� ������ /////////////////////////////////////////////////

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

//////////////////////// ������ �ִϸ��̼� �̺�Ʈ�� �Լ� ������ /////////////////////////////////////////////////
    }
}


