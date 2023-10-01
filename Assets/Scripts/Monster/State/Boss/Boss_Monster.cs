using player;
using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace boss
{
    enum B_State
    {
        IDLE = 0,
        CHASE,
        ATTACK_1,
        ATTACK_2,
        SKILL_1,
        SKILL_2,
        SKILL_3,
        DIE,
        GROGGY
    }
    public class Boss_Monster : MonoBehaviour
    {
        public Transform target { get; set; }                       //몬스터가 쫒는 목표(플레이어)
        public float chaseSpeed = 2.0f;
        public float speed = 1.0f;                  //몬스터 속도
        public float backSpeed = 4.0f;              //몬스터가 스폰포지션으로 돌아가는 속도
        public float gravity = -9.81f;                     // 중력
        public Quaternion targetRotation;                                //플레이어의 방향 멤버 변수
        public float rotationSpeed = 200f;          //타겟을 쳐다보는데 걸리는 속도
        public float distance;


        Vector3 spawnPosition;
        public Vector3 SpawnPosition
        {
            get => spawnPosition;
            set
            {
                spawnPosition = value;
                Debug.Log($"{spawnPosition} 스폰 포즈 세팅");
            }
        }
        public Vector3 dir;
        public Vector3 moveDirection;
        public Vector3 rotation;
        public Vector3 direction;
        public PlayerController player;
        public Monster_FOV_1 FOV1;
        public Monster_FOV_2 FOV2;
        public Attack_FOV attack_FOV;
        public NavMeshAgent nav;
        Animator animator;
        

        readonly int AnimatorState = Animator.StringToHash("State");
        readonly int DieState = Animator.StringToHash("Die");
        readonly int AttackState = Animator.StringToHash("Attack");


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
                if (hp <= 0)
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
            FOV1 = GetComponent<Monster_FOV_1>();
            FOV2 = GetComponent<Monster_FOV_2>();
            attack_FOV = GetComponent<Attack_FOV>();
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
            
        }
        void Start()
        {
            idleState.EnterState();
        }

        void OnEnable()
        {
        
        }



        public void MonsterAnimatorChange(int state)
        {
            animator.SetInteger(AnimatorState, state);
        }
        public void MonsterDieChange(bool isChange)
        {
            animator.SetBool(DieState, isChange);
        }
        



        protected virtual void FixedUpdate()
        {
            monsterCurrentStates.MoveLogic();

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
           
        }



        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("PlayerAttackCollider"))
            {
                HP -= 1;
            }
        }


    }
}


