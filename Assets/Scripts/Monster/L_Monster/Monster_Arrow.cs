using l_monster;
using player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster_Arrow : MonoBehaviour
{
    MonsterStatsActor monsterStatsActor;
    PlayerController player;
    Transform tagetPos;
    Vector3 dir;
    float speed;
    

    bool IsCollision = false;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        monsterStatsActor = FindObjectOfType<MonsterStatsActor>();
        Transform child = player.transform.GetChild(0);
        tagetPos = child.transform;
        speed = 3;
        
    }

    private void Start()
    {
        dir = (tagetPos.position - transform.position).normalized;
        StartCoroutine(LifeOver(4));
    }
    IEnumerator LifeOver(float delay = 0.0f)
    {
        yield return new WaitForSeconds(delay); // delay��ŭ ����ϰ�
        Destroy(this.gameObject);            // ���� ������Ʈ ��Ȱ��ȭ
        Debug.Log("ȭ�� ����");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !IsCollision)
        {
            monsterStatsActor.OnDamage(25);
            transform.Translate(Vector3.zero);
           
            
            IsCollision = true;

        }
    }

    private void Update()
    {
        if(!IsCollision)
        {
            transform.Translate(dir * Time.deltaTime * speed, Space.World);
        }
    }
}
