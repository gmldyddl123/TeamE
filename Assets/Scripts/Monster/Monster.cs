using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Monster : MonoBehaviour
{
    //몬스터가 쫒는 목표(플레이어)
    Transform target;
    //몬스터가 플레이어를 추격하는 속도
    public float speed = 2.0f;
    //몬스터가 스폰포지션으로 돌아가는 속도
    public float backSpeed = 4.0f;

    //몬스터가 스폰포지션으로 돌아가는 속도조절을 위한 멤버변수
    float velocity;
    float gravity = -9.81f; // 중력
    //플레이어의 방향 멤버 변수
    private Quaternion targetRotation;
    //타겟을 쳐다보는데 걸리는 속도
    public float rotationSpeed = 5.0f;
    //몬스터와 플레이어의 최대 근접 거리 및 공격발동 거리
    public float Distance = 1;
    //몬스터의 스폰 포지션
    Vector3 spawnPosition = Vector3.zero;
    //스폰포지션의 방향
    private Quaternion spawnRotation;
    //몬스터의 현재 상태에 대한 플래그
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
   /// 몬스터가 리스폰 지역으로 돌아가는 함수
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
    /// 플레이어와 몬스터가 3초이상 몬스터 인식범위에서 떨어져있을시 발동되는 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator Stop()
    {
        yield return new WaitForSeconds(3);
        targetOn = false;
        runAway = true;
    }
      
    /// <summary>
    /// 몬스터가 플레이어를 추격하는 함수
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


