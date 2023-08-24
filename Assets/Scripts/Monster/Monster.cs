using player;
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
        
    }
    public class Monster : PooledObject
    {
        public Transform target { get; set; }                       //���Ͱ� �i�� ��ǥ(�÷��̾�)
        public float chaseSpeed = 2.0f;
        public float speed  = 1.0f;                  //���� �ӵ�
        public float backSpeed  = 4.0f;              //���Ͱ� �������������� ���ư��� �ӵ�
        public float gravity  = -9.81f;                     // �߷�
        public Quaternion targetRotation;                                //�÷��̾��� ���� ��� ����
        public float rotationSpeed  = 200f;          //Ÿ���� �Ĵٺ��µ� �ɸ��� �ӵ�
        public float Distance  = 1.2f;              //���Ϳ� �÷��̾��� �ִ� ���� �Ÿ� �� ���ݹߵ� �Ÿ�
        
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
        public NearbyMonster nearbyMonster;

        readonly int AnimatorState = Animator.StringToHash("State");
     
       
     
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
        MonsterState long_AttacktState;              //5
        MonsterState dieState;                    //6
        public MonsterState detectedState;                  //7
        public MonsterState Attack_Ready_M;         //8
        MonsterState attack_Ready_L;             //9
        
        
    

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
        public void Awake()
        {

            nearbyMonster = GetComponent<NearbyMonster>();
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
            
  
        }
        private void Start()
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
        public void MonsterAnimatorChange(int state)
        {
            animator.SetInteger(AnimatorState, state);
        }
        public void MonsterAnimationChange(bool isChange)
        {
            animator.SetBool("Attack",isChange);
        }
        void IsHitAnimation(bool isHit)
        {
            animator.SetBool("Hit", isHit);
        }
       
       

        private void FixedUpdate()
        {
            monsterCurrentStates.MoveLogic();
        }

        

        public void Detected()
        {
            if (onMove && (FOV1.isCollision || FOV2.isCollision))
            {
                //Debug.Log("���⼭����?");
                StopAllCoroutines();
                detectedState.EnterState();
            }
        }
        
    

      
        public System.Action<int> PlusQuestCount;
        public System.Action OnItemDrop;

        void Die()
        {
            nav.enabled = false;
            spawner.SpawnCount--;
            PlusQuestCount?.Invoke(1);
            OnItemDrop?.Invoke();  
            dieState.EnterState();
            OnDisable();

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("PlayerAttackCollider"))
            {
                HP-= 1;
                if(HP > 0)
                {
                    IsHitAnimation(true);
                }
                Debug.Log($"���� HP�� {HP} �̴�.");
                monsterEvents.MonsterAttacked(this);
            }
        }
    }
}


