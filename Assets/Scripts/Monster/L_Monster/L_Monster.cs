using player;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace l_monster
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
        
        public Transform target { get; set; }       //���Ͱ� �i�� ��ǥ(�÷��̾�)
       
        
        public float chaseSpeed = 2.0f;
        public float speed = 1.0f;                  //���� �ӵ�
        public float backSpeed = 4.0f;              //���Ͱ� �������������� ���ư��� �ӵ�
        public Quaternion targetRotation;           //�÷��̾��� ���� ��� ����
        
        public float rotationSpeed = 200f;          //Ÿ���� �Ĵٺ��µ� �ɸ��� �ӵ�

        public ItemDropController itemDropController;  // �߰�

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
        public L_FOV_1 FOV1;
        public NavMeshAgent nav;
        CharacterController characterController;
        public Animator animator;
        public L_Spawner spawner;
        public MonsterEvent monsterEvents;
        public bool animatorAttack;
        public NearbyMonsterAttacked nearbyMonster;
        DisappearArrow disappearArrow;
        ArrowShoot  arrowShoot;
        
       

        readonly  int AnimatorState = Animator.StringToHash("State");
        readonly  int DieState = Animator.StringToHash("Die");
        readonly  int AttackState = Animator.StringToHash("Attack");



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
        public MonsterState long_AttackState;        //4
        MonsterState dieState;                    //6
        public MonsterState detectedState;                  //7
        public MonsterState attack_Ready;         //8
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
                if (hp <= 0)
                {
                    hp = 0;
                    dieState.EnterState();
                }
                hp = Mathf.Clamp(hp, 0, MaxHP);
                onHealthChange?.Invoke(hp / MaxHP);
            }
        }
        void Awake()
        {
            nearbyMonster = GetComponent<NearbyMonsterAttacked>();
            nav = GetComponent<NavMeshAgent>();
            FOV1 = GetComponentInChildren<L_FOV_1>();
            player = FindObjectOfType<PlayerController>();
            spawner = FindObjectOfType<L_Spawner>();
            disappearArrow = FindObjectOfType<DisappearArrow>();
            target = player.transform;
            animator = GetComponent<Animator>();
            monsterEvents = FindObjectOfType<MonsterEvent>();
            arrowShoot = FindObjectOfType<ArrowShoot>();
            itemDropController = FindObjectOfType<ItemDropController>();  // �߰�


            //arrowPosition = arrowShootPosition.position;
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

           
            monsterEvents.OnMonsterAttacked += nearbyMonster.ReactToMonsterAttack;

        }
        void Start()
        {
            nav.speed = speed;
            nav.angularSpeed = 200;
            onMove = true;
            isStop = true;
            isAttack = false;
            FOV1.detected_1 += Detected;
            idleState.EnterState();
        }

        void OnEnable()
        {
            nav.enabled = true;
            characterController.enabled = true;
            FOV1.gameObject.SetActive(true);
            disappearArrow.gameObject.SetActive(false);
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
            if (onMove && FOV1.isCollision)
            {
                detectedState.EnterState();
            }
        }




        public Action<int> L_PlusQuestCount;
     

        /// <summary>
        /// ���Ͱ� ������ Disableó���� ���� �Լ�
        /// </summary>
        public void AfterDie()
        {
            OnDisable();
        }

        public void Die()
        {
            characterController.enabled = false;
            FOV1.gameObject.SetActive(false);
            nav.enabled = false;
            monsterEvents.SpawnCountChange?.Invoke();
            monsterEvents.PlusQuestCount?.Invoke(1);
            monsterEvents.OnItemDrop?.Invoke();
            // ������ ��� ������ ȣ��
            itemDropController?.RandomDropItems(); // RandomDropItems �Լ��� ȣ��
            monsterEvents.OnItemDrop?.Invoke();
        }


        //Test��
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("PlayerAttackCollider"))
            {
                HP -= 10;
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

        public void ArrowEnable()
        {
            disappearArrow.gameObject.SetActive(true);
        }
        public void ArrowDisable()
        {
            disappearArrow.gameObject.SetActive(false);
        }

        public void ArrowShootStart()
        {
            long_AttackState.EnterState();
        }
       
        public void ArrowShooting()
        {
            arrowShoot.ArrowShooting();
        }
    }
}



