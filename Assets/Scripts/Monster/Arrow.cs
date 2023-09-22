using l_monster;
using player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    L_Monster monster;
    PlayerController player;
    Transform startPos;
    Transform tagetPos;
    Vector3 dir;
    float speed;
    

    bool IsCollision = false;

    private void Awake()
    {
        monster = FindObjectOfType<L_Monster>();
        player = FindObjectOfType<PlayerController>();
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
