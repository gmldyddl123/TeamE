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

        public CharacterController bossCollider;

        public PlayerController player;
        public Monster_FOV_1 FOV1;
        public Monster_FOV_2 FOV2;

        public NavMeshAgent nav;
        public Animator animator;

        public bool isAtkCooldown = true;
        public bool isAttack = false;
        public bool Weapondive = false;
        public bool isSkillCooldown = true;
        public bool isGroggy = false;
        public bool Phaze_2 = false;

        public GameObject atk_1_Weapon;
        public GameObject atk_2_Weapon;
        public GameObject skill_Weapon;

        Vector3 skill_Weapon_Pos;

        ParticleSystem atk_1;
        ParticleSystem atk_2;
        ParticleSystem skill_1;
        ParticleSystem skill_2;
        public ParticleSystem skill_3;

        

        readonly int AnimatorState = Animator.StringToHash("State");
        readonly int DieState = Animator.StringToHash("Die");

        float skillCooldownTime;
        float skillCoolTime = 5;

        public float atkCooldownTime;
        public float atkCoolTime = 2;

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
                if (groggy <= 0)
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
                if (hp <= MaxHP * 0.5) 
                {
                    bossCollider.enabled = false;
                    Phaze_2 = true;
                    skillCoolTime = 3;
                    atkCoolTime = 1;
                    skill_2_State.EnterState();
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
            atk_1 = child.GetComponent<ParticleSystem>();
            child = transform.GetChild(2).GetChild(1);
            atk_2 = child.GetComponent<ParticleSystem>();
            child = transform.GetChild(2).GetChild(3);
            skill_1 = child.GetComponent<ParticleSystem>();
            child = transform.GetChild(2).GetChild(4);
            skill_2 = child.GetComponent<ParticleSystem>();
            child = transform.GetChild(2).GetChild(5);
            skill_3 = child.GetComponent<ParticleSystem>();

            skill_Weapon_Pos = skill_Weapon.transform.position;

            player = FindObjectOfType<PlayerController>();

            target = player.transform;
            animator = GetComponent<Animator>();
            

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
        public void MonsterDieChange(bool isChange)
        {
            animator.SetBool(DieState, isChange);
        }
        public void MonsterGroggyChange(string name)
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
                HP -= 1;
                Groggy -= 1;
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
            atk_1.Play();
        }
        public void Atk_2_OnEffect()
        {
            atk_2.Play();
        }

        public void Skill_1_OnEffect()
        {
            skill_1.Play();
        }
        public void Skill_2_OnEffect()
        {
            skill_2.Play();
        }

        public void Skill_3_SwordEnable()
        {
            skill_Weapon.SetActive(true);
        }
        public void Skill_3_SwordDisable()
        {
            skill_Weapon.SetActive(false);
            skill_Weapon.transform.position = skill_Weapon_Pos;
        }
        public void Skill_3_SwordAttack()
        {
            Weapondive = true;
        }
        public void Skill_3_OnEffect()
        {
            skill_3.Play();
        }


    }
}


