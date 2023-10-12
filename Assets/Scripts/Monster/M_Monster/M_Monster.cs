using player;
using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace monster
{
    enum M_State
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
    public class M_Monster : Monster_Base
    {
        public Transform target { get; set; }                       //���Ͱ� �i�� ��ǥ(�÷��̾�)
        public float chaseSpeed = 2.0f;
        public float speed  = 1.0f;                  //���� �ӵ�
        public float backSpeed  = 4.0f;              //���Ͱ� �������������� ���ư��� �ӵ�
        public float gravity  = -9.81f;                     // �߷�
        public Quaternion targetRotation;                                //�÷��̾��� ���� ��� ����
        public float rotationSpeed  = 200f;          //Ÿ���� �Ĵٺ��µ� �ɸ��� �ӵ�
        public float distance;
                 
        
        Vector3 spawnPosition;
        public Vector3 SpawnPosition
        {
            get => spawnPosition;
            set 
            {
                spawnPosition = value;
                Debug.Log($"{spawnPosition} ���� ���� ����");
            } 
        }
        public Vector3 dir;
        public Vector3 moveDirection;
        public Vector3 rotation;
        public Vector3 direction;
        public PlayerController player;
        public M_FOV_1 FOV1;
        public M_FOV_2 FOV2;
        public Attack_FOV attack_FOV;
        public NavMeshAgent nav;
        protected CharacterController characterController;
        public Animator animator;
        public M_Spawner spawner;
        public MonsterEvent monsterEvents;
        public bool animatorAttack;
        public NearbyMonsterAttacked nearbyMonster;
        

        readonly int AnimatorState = Animator.StringToHash("State");
        readonly int DieState = Animator.StringToHash("Die");
        readonly int AttackState = Animator.StringToHash("Attack");

        
     
        //���� ����
        public bool onMove = false;
        public bool isAttack = false;
        public bool isback = false;
        public bool isStop = false;
        
       
        public MonsterState monsterCurrentStates;
        public MonsterState idleState;              //0
        public MonsterState walkState;                      //1
        public MonsterState chaseState;             //2
        public MonsterState backState;                       //3
        public MonsterState melee_AttackState;        //4
        protected MonsterState dieState;                    //6
        public MonsterState detectedState;                  //7
        public MonsterState Attack_Ready_M;         //8
        public MonsterState hitState; //10



        float maxHP = 100;
        public float MaxHP => maxHP;
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
                hp = Mathf.Clamp(hp, 0, MaxHP);
                onHealthChange?.Invoke(hp/MaxHP);
            }
        }
       void Awake()
        {
            nearbyMonster = GetComponent<NearbyMonsterAttacked>();
            nav = GetComponent<NavMeshAgent>();
            FOV1 = FindObjectOfType<M_FOV_1>();
            FOV2 = FindObjectOfType<M_FOV_2>();
            attack_FOV = FindObjectOfType<Attack_FOV>();
            player = FindObjectOfType<PlayerController>();
            spawner = FindObjectOfType<M_Spawner>();
            
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
            dieState = new M_DieState(this);
            detectedState = new M_DetectedState(this);
            Attack_Ready_M = new M_AttackReady(this);
            hitState = new M_HitState(this);

            distance = 1.2f;

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
        public void MonsterAttackChange(bool isChange)
        {
            animator.SetBool(AttackState, isChange);
        }
        public void MonsterHittedChange(string name)
        {
            animator.SetTrigger(name);
        }


        protected virtual void FixedUpdate()
        {
            monsterCurrentStates.MoveLogic();

        }


        public override void Detect()
        {
            base.Detect();
            detectedState.EnterState();
        }

        public virtual void Detected()
        {
            if (onMove && (FOV1.isCollision || FOV2.isCollision))
            {
                detectedState.EnterState();
            }
        }
        
    

        /// <summary>
        /// ���Ͱ� ������ Disableó���� ���� �Լ�
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


        // TEST �� �Լ�
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("PlayerAttackCollider") && !isback)
            {
                HP -= 30;
                if (HP > 0)
                {
                    hitState.EnterState();
                }
                isFriendsAttacked = true;
                monsterEvents.MonsterAttacked(this);
                Debug.Log($"{this.name} : ���ݹ���");
            }
        }
        public override void OnDamage(float damage)
        {
            HP -= damage;
            if (HP > 0)
            {
                hitState.EnterState();
            }
            isFriendsAttacked = true;
            monsterEvents.MonsterAttacked(this);
        }

    }
}


