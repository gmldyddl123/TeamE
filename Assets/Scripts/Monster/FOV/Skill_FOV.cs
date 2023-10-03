using player;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using boss;

public class Skill_FOV : MonoBehaviour
{

    PlayerController player;
    Boss_Monster boss;
    
    Transform target;    // ��ä�ÿ� ���ԵǴ��� �Ǻ��� Ÿ��
    public float angleRange = 30f;
    public float radius = 3f;

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
        if(boss.isSkil_3_On)
        {
            Vector3 interV = target.position - transform.position;

            // target�� �� ������ �Ÿ��� radius ���� �۴ٸ�
            if (interV.magnitude < radius)
            {
                // 'Ÿ��-�� ����'�� '�� ���� ����'�� ����
                float dot = Vector3.Dot(interV.normalized, transform.forward);
                // �� ���� ��� ���� �����̹Ƿ� ���� ����� cos�� ���� ���ؼ� theta�� ����
                float theta = Mathf.Acos(dot);
                // angleRange�� ���ϱ� ���� degree�� ��ȯ
                float degree = Mathf.Rad2Deg * theta;

                // �þ߰� �Ǻ�
                if (degree <= angleRange * 0.5f)
                {
                    isCollision = true;
                    boss.OnSkillHit?.Invoke();
                }

                else
                    isCollision = false;

            }
            else
                isCollision = false;
        }

    }
  

    // ����Ƽ �����Ϳ� ��ä���� �׷��� �޼ҵ�
    private void OnDrawGizmos()
    {
        Handles.color = isCollision ? _yellow : _lightBlue;
        // DrawSolidArc(������, ��ֺ���(��������), �׷��� ���� ����, ����, ������)
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, angleRange / 2, radius);
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -angleRange / 2, radius);
    }
}
