using player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    float gravity = -9.81f; // �߷�
    //�÷��̾��� ���� ��� ����
    private Quaternion targetRotation;
    //Ÿ���� �Ĵٺ��µ� �ɸ��� �ӵ�
    public float rotationSpeed = 5.0f;
    //���Ϳ� �÷��̾��� �ִ� ���� �Ÿ� �� ���ݹߵ� �Ÿ�
    public float Distance = 1;
    //������ ���� ������
    protected Vector3 spawnPosition = Vector3.zero;
    //������������ ����
    private Quaternion spawnRotation;
    //������ ���� ���¿� ���� �÷���
    bool targetOn = false;
    bool runAway = false;
    
    Vector3 dir;
    Vector3 moveDirection;
    Vector3 targetPosition;
    Vector3 areaMin = new Vector3(-7.5f, 0,2.5f);
    
   Vector3 areaMax = new Vector3(-2.5f,0 ,7.5f);


    Vector3 direction;
    Animator animator;
    GameObject weapon;
    PlayerInputSystem player;
  
    CharacterController characterController;
    Spawner spawner;





    private void Awake()
    {
     
        player = FindObjectOfType<PlayerInputSystem>();
        target = player.transform;
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        //weapon = transform.GetChild(3).gameObject;
        spawnPosition = transform.position;
        spawner = FindObjectOfType<Spawner>();
        
    }
    private void Start()
    {
        //StartCoroutine(Move());  
        SetMove();
    }

    private void Update()
    {
        if (targetOn)
        {
            
            MoveToTarget();
        }
        if (runAway)
        {
            
            BackToRespawn();
        }
        else if (targetOn==false && runAway ==false)
        {
            OnMoveUpdate();
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

           
            if (characterController.isGrounded == false)
            {
                direction.y += gravity * Time.fixedDeltaTime;
            }


            characterController.Move(direction * speed * Time.fixedDeltaTime);
        }
        if (distance < 1f)
        {
            runAway = false;
            recogArea.enabled = true;
            
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
    IEnumerator Back()
    {
        yield return new WaitForSeconds(5);
        targetOn = false;
        BackToRespawn();
    }
    void OnMoveUpdate()
    {
            
            moveDirection.y = 0;
            targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (characterController.isGrounded == false)
            {
                moveDirection.y += gravity * Time.fixedDeltaTime;
            }

            characterController.Move(dir * speed * Time.fixedDeltaTime);


            if (transform.position.z > areaMax.z)
            {
                SetMove();

            }
            else if (transform.position.z < areaMin.z)
            {
                SetMove();

            }
        
    }
    // IEnumerator Move()
    //{
    //    float moveSpeed = speed;
    //    while (true)

    //    {
    //        if (targetOn == false && runAway == false)
    //        { 
    //        SetMove();
    //        OnMoveUpdate();
    //        yield return new WaitForSeconds(2);
    //        speed = 0;
    //        SetMove();
    //        OnMoveUpdate();
    //        yield return new WaitForSeconds(2);
    //        speed = moveSpeed;
    //         }
           
    //    }
    //}
    void SetMove()
    {

        
            float x;
            float z;

            x = Random.Range(areaMin.x, areaMax.x);
            if (transform.position.z > 5)
            {
                z = areaMin.z;
            }
            else
            {
                z = areaMax.z;
            }
            targetPosition = new Vector3(x,0,z);
            moveDirection = targetPosition - transform.position;   
            dir = moveDirection.normalized;
            
          
         
        
    }

   
}


