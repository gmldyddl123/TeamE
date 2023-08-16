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
        Die
    }
    public class Monster : PooledObject
    {
        public Transform target { get; set; }                       //���Ͱ� �i�� ��ǥ(�÷��̾�)
        public float speed  = 2.0f;                  //���� �ӵ�
        public float backSpeed  = 4.0f;              //���Ͱ� �������������� ���ư��� �ӵ�
        public float gravity  = -9.81f;                     // �߷�
        public Quaternion targetRotation;                                //�÷��̾��� ���� ��� ����
        public float rotationSpeed  = 5.0f;          //Ÿ���� �Ĵٺ��µ� �ɸ��� �ӵ�
        public float Distance  = 1.2f;                  //���Ϳ� �÷��̾��� �ִ� ���� �Ÿ� �� ���ݹߵ� �Ÿ�
        float wait = 2;
        public Vector3 SpawnPosition { get; set; }
        public Vector3 dir;
        public Vector3 moveDirection;
        public Vector3 patrolTargetPosition;
        public Vector3 rotation;
        public Vector3 direction;
        PlayerController player;
        public Monster_FOV_1 FOV1;
        public Monster_FOV_2 FOV2;
        public Attack_FOV attack_FOV;
        public NavMeshAgent nav;
        public CharacterController characterController;
        public Animator animator;
        public Spawner spawner;
        protected MonsterEvent monsterEvents;
        public bool animatorAttack;

        readonly int AnimatorState = Animator.StringToHash("State");
     
       
     
        //���� ����
        public bool onMove = false;
        public bool isAttack = false;
        
        public MonsterState monsterCurrentStates;
        public MonsterState idleState;              //0
        MonsterState walkState;       //1
        public MonsterState chaseState;             //2
        MonsterState backState;              //3
        public MonsterState melee_AttackState;      //4
        public MonsterState Attack_Ready_M;
        MonsterState long_AttacktState;      //5
        MonsterState dieState;               //6
        public Transform startpoint;

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
                    Die();
                }
            }
        }
        public void Awake()
        {
            startpoint = transform;
            nav = GetComponent<NavMeshAgent>();
            FOV1 = FindObjectOfType<Monster_FOV_1>();
            FOV2= FindObjectOfType<Monster_FOV_2>();
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
            Attack_Ready_M = new M_AttackReady_M(this);
            long_AttacktState = new M_LongAttackState();
            dieState = new M_DieState(this);


            monsterCurrentStates = idleState;
        
            MonsterAnimatorChange(0);
        }
        private void Start()
        {
           
            onMove = true;
            isAttack = false;
            //StartCoroutine(OnMove());
         
        }
        public void MonsterAnimatorChange(int state)
        {
            animator.SetInteger(AnimatorState, state);
        }
        public void MonsterAnimationChange(bool isChange)
        {
            animator.SetBool("Attack",isChange);
        }
       
       

        private void FixedUpdate()
        {

            Detected();
            monsterCurrentStates.MoveLogic();
        }


        public void Patrol()
        {
            
            StartCoroutine(patrol());
        }

        IEnumerator patrol()
        {
            
            yield return new WaitForSeconds(wait);
           
            walkState.EnterState();
        }

     

        //public IEnumerator OnMove()
        //{
        //    wait = Random.Range(3, 7);
        //    while (true)
        //    {
        //        idleState.EnterState();
        //        yield return new WaitForSeconds(wait * 0.5f);
        //        walkState.EnterState();
        //        yield return new WaitForSeconds(wait);

        //    }

        // }
        //public void moveHelper()
        //{

        //    StartCoroutine(OnMove());
        //}

        /// <summary>
        /// ���Ͱ� ������������ ���� �Ѵ� �ڷ�ƾ
        /// </summary>
        /// <returns></returns>
        public IEnumerator BackToSpawn()
        {
            yield return new WaitForSeconds(1.5f);
            nav.ResetPath();
            backState.EnterState();
        }

        public void Back()
        {
            StartCoroutine(BackToSpawn());
        }

        public void Detected()
        {
            if ((FOV1.isCollision || FOV2.isCollision) && onMove)
            {
                StopAllCoroutines();
                nav.ResetPath();
                onMove = false;
                chaseState.EnterState();
            }
            //if (!FOV1.isCollision && !FOV2.isCollision && onMove == false)
            //{
            //    if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            //    {
            //    Back();
            //    onMove = true;
            //    }
            //}
        }

      
        public System.Action<int> PlusQuestCount;
        public System.Action OnItemDrop;

        void Die()
        {
            dieState.EnterState();
            spawner.spawnCount--;
            PlusQuestCount?.Invoke(1);
            OnItemDrop?.Invoke();  
            OnDisable();

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("PlayerAttackCollider"))
            {
                // �÷��̾��� ������ �޾��� �� �̺�Ʈ�� �߻���Ŵ
                HP -= 50;
                Debug.Log($"���� HP�� {HP} �̴�.");
                monsterEvents.MonsterAttacked(this);
                chaseState.EnterState();
            }
        }

    

    }
}


