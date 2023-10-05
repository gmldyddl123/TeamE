using player;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using boss;

public class Skill_1_FOV : MonoBehaviour
{
    PlayerController player;
    Boss_Monster boss;
    
    Transform target;    // 부채꼴에 포함되는지 판별할 타겟
    public float angleRange = 30f;
    public float radius = 3f;

    float time;
    Color _lightBlue = new Color(0f, 1f, 1f, 0.2f);
    Color _yellow = new Color(1f, 1f, 0f, 0.2f);

    public bool isCollision = false;
 
    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        target = player.transform;
        boss = GetComponentInParent<Boss_Monster>();

    }
    void Update()
    {
        
        if(boss.isSkil_1_On)
        {
            time += Time.deltaTime;
            Vector3 interV = target.position - transform.position;

            // target과 나 사이의 거리가 radius 보다 작다면
            if (interV.magnitude < radius)
            {
                // '타겟-나 벡터'와 '내 정면 벡터'를 내적
                float dot = Vector3.Dot(interV.normalized, transform.forward);
                // 두 벡터 모두 단위 벡터이므로 내적 결과에 cos의 역을 취해서 theta를 구함
                float theta = Mathf.Acos(dot);
                // angleRange와 비교하기 위해 degree로 변환
                float degree = Mathf.Rad2Deg * theta;

                // 시야각 판별
                if (degree <= angleRange * 0.5f)
                {
                    isCollision = true;
                    if(time > 0.2)
                    {
                        boss.OnSkill_1_Hit?.Invoke();
                        time = 0;
                        Debug.Log("다다닥 맞는다");
                    }
                }

                else
                {
                    isCollision = false;
                    time = 0;
                }             
            }
            else
            {
                isCollision = false;
                time = 0;
            }
        }

    }
  

    // 유니티 에디터에 부채꼴을 그려줄 메소드
    private void OnDrawGizmos()
    {
        Handles.color = isCollision ? _yellow : _lightBlue;
        // DrawSolidArc(시작점, 노멀벡터(법선벡터), 그려줄 방향 벡터, 각도, 반지름)
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, angleRange / 2, radius);
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -angleRange / 2, radius);
    }
}
