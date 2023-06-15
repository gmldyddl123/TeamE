using player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

namespace monster
{
    enum State
    {
        IDLE = 0,
        WALK,
        CHASE,
        MELEE_ATTACK,
        LONG_ATTACK,
        Die
    }
    public class MonsterTEST : MonoBehaviour
    {
        public Transform target;                       //���Ͱ� �i�� ��ǥ(�÷��̾�)
        public float speed { get; private set; } = 2.0f;                  //���� �ӵ�
        public float backSpeed { get; private set; } = 4.0f;              //���Ͱ� �������������� ���ư��� �ӵ�
        public float gravity { get; private set; } = -9.81f;                     // �߷�
        public Quaternion targetRotation;                                //�÷��̾��� ���� ��� ����
        public float rotationSpeed { get; private set; } = 5.0f;          //Ÿ���� �Ĵٺ��µ� �ɸ��� �ӵ�
        public float Distance { get; private set; } = 1;                  //���Ϳ� �÷��̾��� �ִ� ���� �Ÿ� �� ���ݹߵ� �Ÿ�
        public Quaternion spawnRotation;                                 //������������ ����

        public Vector3 spawnPosition;
        public Vector3 dir;
        public Vector3 moveDirection;
        public Vector3 targetPosition;
        public Vector3 areaMin { get; private set; } = new Vector3(-7.5f, 0, 2.5f);
        public Vector3 areaMax { get; private set; } = new Vector3(-2.5f, 0, 7.5f);
        public Vector3 direction;
        PlayerInputSystem player;
        public CharacterController characterController;
        Animator animator;
        readonly int AnimatorState = Animator.StringToHash("State");
        //���� ����

        public MonsterState monsterCurrentStates;
        MonsterState idleState;
        MonsterState walkState;
        MonsterState chaseState;
        MonsterState melee_AttackState;
        MonsterState long_AttacktState;
        MonsterState dieState;

        public void Awake()
        {
     
            player = FindObjectOfType<PlayerInputSystem>();
            target = player.transform;
            animator = GetComponent<Animator>();
            characterController = GetComponent<CharacterController>();
            spawnPosition = transform.position;

            idleState = new M_IdleState(this);
            walkState = new M_WalkState(this);
            chaseState = new M_ChaseState(this);
            melee_AttackState = new M_MeleeAttackState(this);
            long_AttacktState = new M_LongAttackState();
            dieState = new M_DieState();


            monsterCurrentStates = idleState;
            PlayerAnimoatrChage(0);
        }
        private void Start()
        { 
            //walkState.EnterState();
           // walkState.MoveLogic();
        }
        public void PlayerAnimoatrChage(int state)
        {
            animator.SetInteger(AnimatorState, state);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                //targetOn = true;
                chaseState.EnterState();
               // chaseState.MoveLogic();
            }

        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(Stop());
            }
        }
        /// <summary>
        /// ���Ͱ� ������ �������� ���ư��� �Լ�
        /// </summary>
        void BackToRespawn()
        {

            Transform recog = transform.GetChild(2);

            Collider recogArea = recog.GetComponent<Collider>();

            recogArea.enabled = false;

            Vector3 direction = spawnPosition - transform.position;
            direction.y = 0;
            if (direction != Vector3.zero)
            {
                spawnRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, spawnRotation, rotationSpeed * Time.deltaTime);
            }

            float distance = Vector3.Distance(spawnPosition, transform.position);
            if (distance > 0)
            {
                direction = (spawnPosition - transform.position).normalized;


                if (characterController.isGrounded == false)
                {
                    direction.y += gravity * Time.fixedDeltaTime;
                }


                characterController.Move(direction * speed * Time.fixedDeltaTime);
            }
            if (distance < 1f)
            {
                //runAway = false;
                recogArea.enabled = true;
                walkState.EnterState();
               // walkState.MoveLogic();

            }


        }
        private void FixedUpdate()
        {
            Debug.Log(monsterCurrentStates);
            monsterCurrentStates.MoveLogic();
        }

      
        /// <summary>
        /// �÷��̾�� ���Ͱ� 3���̻� ���� �νĹ������� ������������ �ߵ��Ǵ� �ڷ�ƾ
        /// </summary>
        /// <returns></returns>
        IEnumerator Stop()
        {
            yield return new WaitForSeconds(2);
           BackToRespawn();
            
        }

        public void Attack()
        {
          
        }

        protected virtual void Die()
        {
           
        }
     
       

    }
}

