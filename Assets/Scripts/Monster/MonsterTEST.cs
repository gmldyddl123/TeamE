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
        BACK,
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
        MonsterState idleState;              //0
        public MonsterState walkState;              //1
        MonsterState chaseState;             //2
        MonsterState backState;              //3
        MonsterState melee_AttackState;      //4
        MonsterState long_AttacktState;      //5
        MonsterState dieState;               //6
            
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
            backState = new M_BackState(this);
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
                chaseState.EnterState();
            }

        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                
            }
        }

        private void FixedUpdate()
        {
            Debug.Log(monsterCurrentStates);
            monsterCurrentStates.MoveLogic();
        }

      
      

        public void Attack()
        {
          
        }

        protected virtual void Die()
        {
           
        }
     
       

    }
}

