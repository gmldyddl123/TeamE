using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Monster : MonoBehaviour
{
    //���Ͱ� �i�� ��ǥ(�÷��̾�)
    Transform target;
    //���Ͱ� �÷��̾ �߰��ϴ� �ӵ�
    public float speed = 2.0f;
    //���Ͱ� �������������� ���ư��� �ӵ�
    public float backSpeed = 4.0f;

    //���Ͱ� �������������� ���ư��� �ӵ������� ���� �������
    float velocity;
    float gravity = -9.81f; // �߷�
    //�÷��̾��� ���� ��� ����
    private Quaternion targetRotation;
    //Ÿ���� �Ĵٺ��µ� �ɸ��� �ӵ�
    public float rotationSpeed = 5.0f;
    //���Ϳ� �÷��̾��� �ִ� ���� �Ÿ� �� ���ݹߵ� �Ÿ�
    public float Distance = 1;
    //������ ���� ������
    Vector3 spawnPosition = Vector3.zero;
    //������������ ����
    private Quaternion spawnRotation;
    //������ ���� ���¿� ���� �÷���
    bool targetOn = false;
    bool runAway = false;
   
    Vector3 direction;
    Animator animator;
    GameObject weapon;
    PlayerController player;
    Monter_Move monster_Move;
    CharacterController characterController;





    private void Awake()
    {
        monster_Move = FindObjectOfType<Monter_Move>();
        player = FindObjectOfType<PlayerController>();
        target = player.transform;
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        //weapon = transform.GetChild(3).gameObject;
        spawnPosition = transform.position;
    }
    private void Start()
    {
        //StartCoroutine(monster_Move.Move());
    }

    private void Update()
    {
        if (targetOn)
        { 
            MoveToTarget();
        }
        if(runAway)
        {
            BackToRespawn();
        }
       



    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetOn = true;

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            StartCoroutine(Stop());
            


        }
        //if(other.CompareTag("SpawnArea"))
        //{
        //    StartCoroutine(Stop() );
        //}
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

            //velocity = backSpeed * Time.deltaTime;

            //transform.position = new Vector3(transform.position.x + (direction.x * velocity),
            //                                       transform.position.y + (direction.y * velocity),
            //                                          transform.position.z + (direction.z * velocity));
            if (characterController.isGrounded == false)
            {
                direction.y += gravity * Time.fixedDeltaTime;
            }


            characterController.Move(direction * speed * Time.fixedDeltaTime);
        }
       if(distance < 1f)
        {
            runAway = false;
            recogArea.enabled = true;
            //StartCoroutine(monster_Move.Move());
        }
           
       
    }

    /// <summary>
    /// �÷��̾�� ���Ͱ� 3���̻� ���� �νĹ������� ������������ �ߵ��Ǵ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator Stop()
    {
        yield return new WaitForSeconds(3);
        targetOn = false;
        runAway = true;
    }
      
    /// <summary>
    /// ���Ͱ� �÷��̾ �߰��ϴ� �Լ�
    /// </summary>
    public void MoveToTarget()
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0; 
        targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); 

        float distance = Vector3.Distance(target.position, transform.position);
        if (distance > Distance)
        {
            direction = (target.position - transform.position).normalized;

            //velocity = speed * Time.deltaTime;

            //transform.position = new Vector3(transform.position.x + (direction.x * velocity),
            //                                       transform.position.y + (direction.y * velocity),
            //                                          transform.position.z + (direction.z * velocity));


            if (characterController.isGrounded == false)
            {
                direction.y += gravity * Time.fixedDeltaTime;
            }


            characterController.Move(direction * speed * Time.fixedDeltaTime);
        }
        if (distance <= Distance)
        {
            Attack();
        }
        
    }
    
    public void Attack()
    {
        
    }
    
    protected virtual void Die()
    {
        Destroy(gameObject);
    }

}


