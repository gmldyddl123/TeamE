using player;
using System;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.UIElements;

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
        public Transform target { get; set; }                       //몬스터가 쫒는 목표(플레이어)
        
        public float rotationSpeed = 8;
        public CharacterController bossCollider;

        public PlayerController player;
        public Monster_FOV_1 FOV1;
        public Monster_FOV_2 FOV2;

        public NavMeshAgent nav;
        public Animator animator;

        public bool isAtkCooldown { get; set; } = true;
        public bool isAttack { get; set; } = true;
        public bool Weapondive { get; set; } = false;
        public bool isSkillCooldown { get; set; } = true;
        public bool isGroggy { get; set; } = false;
        public bool Phaze_2 { get; set; } = false;
        public bool isSkill { get; set; } = false;
        public bool isPhaze2Success { get; set; } = false;
        public bool isGroggySuccess { get; set; } = false;
        public bool isSkil_3_On { get; set; } = false;
        public bool isSkil_1_On { get; set; } = false;

        public GameObject atk_1_Weapon;
        public GameObject atk_2_Weapon;
        public GameObject skill_Weapon;

        //Vector3 skill_Weapon_Pos;
        //Quaternion skill_Weapon_Rot;

        GameObject atk_1;
        GameObject atk_2;
        GameObject skill_1;
        GameObject skill_2;
        GameObject skill_3;

        readonly int AnimatorState = Animator.StringToHash("State");

        public float skillCooldownTime;
        public float skillCoolTime;

        public float atkCooldownTime;
        public float atkCoolTime;

        public MonsterState monsterCurrentStates;
        public MonsterState idleState;              
        public MonsterState chaseState;                                    
        public MonsterState attack_1_State;                                    
        public MonsterState attack_2_State;                                    
        public MonsterState skill_1_State;                                    
        public MonsterState skill_2_State;                                    
        public MonsterState skill_3_State;                                    
        public MonsterState dieState;                    
        public MonsterState groggyState;

        public Action isPhaze2 { get; set; }
        public Action isSkill_1_Hit_Start { get; set; }
        //public Action isSkill_3_Hit_Start { get; set; }
        public Action isSkill_1_Hit_Finish { get; set; }
        //public Action isSkill_3_Hit_Finish { get; set; }
        public Action OnSkill_3_Hit { get; set; }
        public Action OnSkill_1_Hit { get; set; }
        public Action<float> bossHealthChange { get; set; }
        public Action<float> bossGroggyChange { get; set; }

        float maxGroggy = 10;
        public float MaxGroggy =>maxGroggy;
        float groggy = 10;
        public float Groggy
        {
            get => groggy;
            set
            {
                groggy = value;
                if (groggy <= 0 && (monsterCurrentStates != groggyState))
                { 
                    groggy = 0;
                    isGroggy = true;
                    groggyState.EnterState();
                }
                groggy = Mathf.Clamp(groggy, 0, MaxGroggy);
                bossGroggyChange?.Invoke(groggy / MaxGroggy);
            }
        }
        float maxHP = 100;
        public float MaxHP => maxHP;
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
            FOV1 = GetComponentInChildren<Monster_FOV_1>();
            FOV2 = GetComponentInChildren<Monster_FOV_2>();
            bossCollider = GetComponent<CharacterController>();

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

            //skill_Weapon_Pos = transform.GetChild(2).GetChild(2).position;
            //skill_Weapon_Rot = transform.GetChild(2).GetChild(2).rotation;

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

            skillCooldownTime = skillCoolTime;
            atkCooldownTime = atkCoolTime;
        }
        void Start()
        {
            idleState.EnterState();
        }


        public void MonsterAnimatorChange(int state)
        {
            animator.SetInteger(AnimatorState, state);
        }
        public void MonsterTriggerChange(string name)
        { 
            animator.SetTrigger(name);
        }
       


        protected virtual void FixedUpdate()
        {
            
            monsterCurrentStates.MoveLogic();

            if (isSkillCooldown)
            {
                skillCooldownTime -= Time.deltaTime;

                if (skillCooldownTime <= 0)
                {
                    isSkillCooldown = false;
                    skillCooldownTime = skillCoolTime;
                }
            }

            if (isAtkCooldown)
            {
                atkCooldownTime -= Time.deltaTime;

                if (atkCooldownTime <= 0)
                {
                    isAtkCooldown = false;
                    atkCooldownTime = atkCoolTime;
                }
            }
        }


        void OnPhaze2()
        {
            if(Groggy <= 0)
            {
                groggyState.EnterState();
            }
            else if(Groggy > 1 && Phaze_2)
            {
                skill_2_State.EnterState();
                Debug.Log("페이즈2시작");
            }
        }

        /// <summary>
        /// 몬스터가 죽은후 Disable처리를 위한 함수
        /// </summary>
        public void AfterDie()
        {
            gameObject.SetActive(false);
        }

        public void Die()
        {
            bossCollider.enabled = false;
        }



        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("PlayerAttackCollider"))
            {
                HP -= 4;
                Groggy -= 1;
                Debug.Log($"현재 HP : {HP}/{MaxHP}, 현재 그로기 게이지 : {Groggy}/ {MaxGroggy}");
                Debug.Log($"{monsterCurrentStates}");
            }
        }


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

        public void Skill_3_SwordEnable()
        {
            //skill_Weapon.SetActive(true);
        }
        public void Skill_3_SwordDisable()
        {
            //skill_Weapon.SetActive(false);
            //skill_Weapon.transform.position = skill_Weapon_Pos;
            //skill_Weapon.transform.rotation = skill_Weapon_Rot;
        }
        public void Skill_3_SwordAttack()
        {
            //Weapondive = true;
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
            isSkill_1_Hit_Start?.Invoke();
        }
        public void Skill_1_Hit_Finish()
        {
            isSkill_1_Hit_Finish?.Invoke();
        }

        public void Skill_3_Hit_Start()
        {
            isSkil_3_On = true;
        }
        public void Skill_3_Hit_Finish()
        {
            isSkil_3_On = false;
        }

        //귀찮아서 뭉쳐놈 효율은 별로인듯?
        public void everySkilloff()
        {
            atk_1.SetActive(false); 
            atk_2.SetActive(false);
            skill_1.SetActive(false);
            skill_3.SetActive(false);
        }
    }
}


