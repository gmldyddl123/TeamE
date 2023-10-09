using boss;
using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_1_Blackhole : MonoBehaviour
{
    float time;
    Vector3 dir;
    Transform target;
    PlayerController player;
    ParticleSystem effect;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        target = player.transform;
        effect = GetComponent<ParticleSystem>();
        
        
    }


    private void Update()
    {
        //아래 내용 Skill_1_State - Moverogic으로 옮기면 괜찮을듯?
        if (effect.isPlaying)
        {
            time += Time.deltaTime;
            // 블랙홀과 풀레이어의 실시간 거리를 측정한다.
            float dis = Vector3.Distance(transform.position, target.position);
            // 1초가 지났을 때(= 블랙홀이 생성되고 1초가 지났을 때)
            if (time > 0.15f)
            {
                // 풀레이어로부터 블랙홀로 향하는 방향을 구한다.
                dir = transform.position - target.position;
                // 풀레이어의 위치를 블랙홀의 방향으로 천천히 이동시킨다.
                target.position += dir * 1f * Time.deltaTime;
            }
        }
        else
        {
            // 재생이 끝나면 time = 0
            time = 0;
        }
    }

}
