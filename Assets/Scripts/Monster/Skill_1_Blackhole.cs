using boss;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_1_Blackhole : MonoBehaviour
{
    Collider coll;
    Boss_Monster boss;
    float time;
    Vector3 dir;
    Vector3 target;
    ParticleSystem effect;

    private void Awake()
    {
        coll = GetComponent<Collider>();
        effect = GetComponent<ParticleSystem>();
        boss = GetComponentInParent<Boss_Monster>();
        target = boss.target.transform.position;
    }
    private void Start()
    {
        boss.isSkill_1_Hit_Start += ColliderOn;
        boss.isSkill_1_Hit_Finish += ColliderOff;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("끌어당겨진드아~");
        }
    }

    private void Update()
    {
        //아래 내용 Skill_1_State - Moverogic으로 옮기면 괜찮을듯? 일단 귀찮음
        if (effect.isPlaying)
        {
            time += Time.deltaTime;
            // 블랙홀과 풀레이어의 실시간 거리를 측정한다.
            float dis = Vector3.Distance(transform.position, target);
            // 1초가 지났을 때(= 블랙홀이 생성되고 1초가 지났을 때)
            if (time > 1)
            {
                // 풀레이어로부터 블랙홀로 향하는 방향을 구한다.
                dir = transform.position - target;
                // 풀레이어의 위치를 블랙홀의 방향으로 천천히 이동시킨다.
                target += dir * 0.1f * Time.deltaTime;
            }
        }
        else
        {
            // 재생이 끝나면 time = 0
            time = 0;
        }
    }

    void ColliderOn()
    {
        coll.enabled = true;
    }
    void ColliderOff()
    {
        coll.enabled = false;
    }
}
