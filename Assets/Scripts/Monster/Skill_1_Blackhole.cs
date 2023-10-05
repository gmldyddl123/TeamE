using boss;
using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_1_Blackhole : MonoBehaviour
{
    //Collider coll;
    //Boss_Monster boss;
    float time;
    Vector3 dir;
    Transform target;
    PlayerController player;
    ParticleSystem effect;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        target = player.transform;
        //coll = GetComponent<Collider>();
        effect = GetComponent<ParticleSystem>();
        //boss = FindObjectOfType<Boss_Monster>();
        
    }
    private void Start()
    {
        //boss.isSkill_1_Hit_Start += ColliderOn;
        //boss.isSkill_1_Hit_Finish += ColliderOff;
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.CompareTag("Player"))
    //    {
    //        Debug.Log("�����������~");
    //    }
    //}

    private void Update()
    {
        //�Ʒ� ���� Skill_1_State - Moverogic���� �ű�� ��������? �ϴ� ������
        if (effect.isPlaying)
        {
            time += Time.deltaTime;
            // ��Ȧ�� Ǯ���̾��� �ǽð� �Ÿ��� �����Ѵ�.
            float dis = Vector3.Distance(transform.position, target.position);
            // 1�ʰ� ������ ��(= ��Ȧ�� �����ǰ� 1�ʰ� ������ ��)
            if (time > 0.15f)
            {
                // Ǯ���̾�κ��� ��Ȧ�� ���ϴ� ������ ���Ѵ�.
                dir = transform.position - target.position;
                // Ǯ���̾��� ��ġ�� ��Ȧ�� �������� õõ�� �̵���Ų��.
                target.position += dir * 1f * Time.deltaTime;
            }
        }
        else
        {
            // ����� ������ time = 0
            time = 0;
        }
    }

    //void ColliderOn()
    //{
    //    coll.enabled = true;
    //}
    //void ColliderOff()
    //{
    //    coll.enabled = false;
    //}
}
