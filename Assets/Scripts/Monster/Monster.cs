using player;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace monster
{
    enum State
    {
        IDLE = 0,
        WALK,
        CHASE,
        BACK,
        MELEE_ATTACK,
        LONG_ATTACK,
        DIE,
        DETECTED,
        ATTACKREADY_M,
        ATTACKREADY_L,
        HIT,
        L_HIT
        
    }
    public class Monster : PooledObject
    {
        public Transform target { get; set; }                       //몬스터가 쫒는 목표(플레이어)
        public float chaseSpeed = 2.0f;
        public float speed  = 1.0f;                  //몬스터 속도
        public float backSpeed  = 4.0f;              //몬스터가 스폰포지션으로 돌아가는 속도
        public float gravity  = -9.81f;                     // 중력
        public Quaternion targetRotation;                                //플레이어의 방향 멤버 변수
        public float rotationSpeed  = 200f;          //타겟을 쳐다보는데 걸리는 속도
        float distance;
        public float Distance   //몬스터와 플레이어의 최대 근접 거리 및 공격발동 거리
        {
            get => distance; 
            set => distance = value;
        }              
        
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
        PlayerController player;
        public Monster_FOV_1 FOV1;
        public Monster_FOV_2 FOV2;
        public Attack_FOV attack_FOV;
        public NavMeshAgent nav;
        CharacterController characterController;
        public Animator animator;
        public Spawner spawner;
        public MonsterEvent monsterEvents;
        public bool animatorAttack;
        public NearbyMonsterAttacked nearbyMonster;

        readonly int AnimatorState = Animator.StringToHash("State");
        readonly int DieState = Animator.StringToHash("Die");
        readonly int AttackState = Animator.StringToHash("Attack");
     
       
     
        //현재 상태
        public bool onMove = false;
        public bool isAttack = false;
        public bool isback = false;
        public bool isStop = false;
        public bool isFriendsAttacked = false;
       
        public MonsterState monsterCurrentStates;
        public MonsterState idleState;              //0
        public MonsterState walkState;                      //1
        public MonsterState chaseState;             //2
        public MonsterState backState;                       //3
        public MonsterState melee_AttackState;        //4
        MonsterState long_AttacktState;              //5
        MonsterState dieState;                    //6
        public MonsterState detectedState;                  //7
        public MonsterState Attack_Ready_M;         //8
        MonsterState attack_Ready_L;             //9
        public MonsterState hitState; //10
        public MonsterState long_HitState; //11
       



        float hp = 100;
        public float HP
        {
            get => hp;
            set
            {
                hp = value;
                if (hp<=0)
                {
                    hp = 0;
                    dieState.EnterState();
                }
            }
        }
       protected virtual void Awake()
        {
            nearbyMonster = GetComponent<NearbyMonsterAttacked>();
            nav = GetComponent<NavMeshAgent>();
            FOV1 = FindObjectOfType<Monster_FOV_1>();
            
            FOV2 = FindObjectOfType<Monster_FOV_2>();
            attack_FOV = FindObjectOfType<Attack_FOV>();
            player = FindObjectOfType<PlayerController>();
            spawner = FindObjectOfType<Spawner>();
            target = player.transform;
            animator = GetComponent<Animator>();
            monsterEvents = FindObjectOfType<MonsterEvent>();
            animatorAttack = animator.GetBool("Attack");
            
            characterController = GetComponent<CharacterController>();
           

            idleState = new M_IdleState(this);
            walkState = new M_WalkState(this);
            chaseState = new M_ChaseState(this);
            backState = new M_BackState(this);
            melee_AttackState = new M_MeleeAttackState(this);
            long_AttacktState = new M_LongAttackState(this);
            dieState = new M_DieState(this);
            detectedState = new M_DetectedState(this);
            Attack_Ready_M = new M_AttackReady_M(this);
            attack_Ready_L = new M_AttackReady_L(this);
            hitState = new HitState(this);
            //long_HitState = new L_HitState(this);

            distance = 1.2f;

        }
        protected virtual void Start()
        {
            nav.speed = speed;
            nav.angularSpeed = 200;
            onMove = true;
            isStop = true;
            isAttack = false;
            FOV1.detected_1 += Detected;
            FOV2.detected_2 += Detected;
            idleState.EnterState();
        }

        protected virtual void OnEnable()
        {
            nav.enabled = true;
            characterController.enabled = true;
            FOV1.gameObject.SetActive(true);
            FOV2.gameObject.SetActive(true);
            attack_FOV.gameObject.SetActive(true);
        }



        public void MonsterAnimatorChange(int state)
        {
            animator.SetInteger(AnimatorState, state);
        }
        public void MonsterDieChange(bool isChange)
        {
            animator.SetBool(DieState, isChange);
        }
        public void MonsterAnimationChange(bool isChange)
        {
            animator.SetBool(AttackState, isChange);
        }



        protected virtual void FixedUpdate()
        {
            monsterCurrentStates.MoveLogic();

        }

        

        public void Detected()
        {
            if (onMove && (FOV1.isCollision || FOV2.isCollision))
            {
                detectedState.EnterState();
            }
        }
        
    

      
        public Action<int> PlusQuestCount;
        public Action OnItemDrop;
        //public Action SpawnCountDown;

        /// <summary>
        /// 몬스터가 죽은후 Disable처리를 위한 함수
        /// </summary>
        public void AfterDie()
        {
            OnDisable();
        }

        public void Die()
        {
            nav.enabled = false;
            characterController.enabled = false;
            FOV1.gameObject.SetActive(false);
            FOV2.gameObject.SetActive(false);
            attack_FOV.gameObject.SetActive(false);
            monsterEvents.SpawnCountChange?.Invoke();
            PlusQuestCount?.Invoke(1);
            OnItemDrop?.Invoke();  
        }
      
        public Action IsHitMaintenance;

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("PlayerAttackCollider"))
            {
                HP -= 1;
                if (HP > 0)
                {
                    hitState.EnterState();
                }
                //Debug.Log($"현재 HP는 {HP} 이다.");
                isFriendsAttacked = true;
                monsterEvents.MonsterAttacked(this);
                Debug.Log($"{this.name} : 공격받음");
            }
        }



    }
}


