using player;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace L_monster
{
    enum L_State
    {
        IDLE = 0,
        WALK,
        CHASE,
        BACK,
        ATTACK,
        DIE,
        ATTACKREADY,
        DETECTED,
        HIT
    }


    public class L_Monster : Monster_Base
    {
        
        public Transform target { get; set; }       //몬스터가 쫒는 목표(플레이어)
        public float chaseSpeed = 2.0f;
        public float speed = 1.0f;                  //몬스터 속도
        public float backSpeed = 4.0f;              //몬스터가 스폰포지션으로 돌아가는 속도
        public float gravity = -9.81f;              // 중력
        public Quaternion targetRotation;           //플레이어의 방향 멤버 변수
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
        protected CharacterController characterController;
        public Animator animator;
        public Spawner spawner;
        public MonsterEvent monsterEvents;
        public bool animatorAttack;
        public NearbyMonsterAttacked nearbyMonster;
        protected DisappearArrow disappearArrow;

        readonly  int AnimatorState = Animator.StringToHash("State");
        readonly  int DieState = Animator.StringToHash("Die");
        readonly  int AttackState = Animator.StringToHash("Attack");



        //현재 상태
        public bool onMove = false;
        public bool isAttack = false;
        public bool isback = false;
        public bool isStop = false;
        

        public MonsterState monsterCurrentStates;
        public MonsterState idleState;              //0
        public MonsterState walkState;                      //1
        public MonsterState chaseState;             //2
        public MonsterState backState;                       //3
        public MonsterState long_AttackState;        //4
        MonsterState dieState;                    //6
        public MonsterState detectedState;                  //7
        public MonsterState attack_Ready;         //8
        public MonsterState hitState; //10





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
            disappearArrow = FindObjectOfType<DisappearArrow>();
            target = player.transform;
            animator = GetComponent<Animator>();
            monsterEvents = FindObjectOfType<MonsterEvent>();
            animatorAttack = animator.GetBool("Attack");

            characterController = GetComponent<CharacterController>();


            idleState = new L_IdleState(this);
            walkState = new L_WalkState(this);
            chaseState = new L_ChaseState(this);
            backState = new L_BackState(this);
            long_AttackState = new L_LongAttackState(this);
            dieState = new L_DieState(this);
            detectedState = new L_DetectedState(this);
            attack_Ready = new L_AttackReady(this);
            hitState = new L_HitState(this);

            distance = 5f;

        }
        void Start()
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

        void OnEnable()
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



        void FixedUpdate()
        {
            monsterCurrentStates.MoveLogic();

        }


        public override void Detect()
        {
            base.Detect();
            detectedState.EnterState();
        }
        public void Detected()
        {
            if (onMove && (FOV1.isCollision || FOV2.isCollision))
            {
                detectedState.EnterState();
            }
        }




        public Action<int> L_PlusQuestCount;
     

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
            monsterEvents.PlusQuestCount?.Invoke(1);
            monsterEvents.OnItemDrop?.Invoke();
        }

       

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("PlayerAttackCollider"))
            {
                HP -= 1;
                if (HP > 0)
                {
                    hitState.EnterState();
                }
                isFriendsAttacked = true;
                monsterEvents.MonsterAttacked(this);
                Debug.Log($"{this.name} : 공격받음");
            }
        }


    }
}



