using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Monster_FOV_Test : MonoBehaviour
{
    //Vector3 dir = new Vector3(3,3,3);
    //RaycastHit hit;
    //Collider m_collider;
    //private void Awake()
    //{
    //    m_collider = GetComponent<Collider>();
    //}
    //private void Update()
    //{
    //    if (Physics.BoxCast(m_collider.bounds.center, transform.localScale, transform.forward,out hit,transform.rotation, 5))
    //    {
    //        Debug.DrawRay(m_collider.bounds.center, hit.point, Color.red);
    //    }
    //}
    public Transform target;    // ��ä�ÿ� ���ԵǴ��� �Ǻ��� Ÿ��
    public float angleRange = 30f;
    public float radius = 3f;

    Color _blue = new Color(0f, 0f, 1f, 0.2f);
    Color _red = new Color(1f, 0f, 0f, 0.2f);

   public bool isCollision = false;

    void Update()
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
            if (degree < angleRange / 2f)
            {
                isCollision = true;
                //detected?.Invoke(true);
            }

            else
                isCollision = false;

        }
        else
            isCollision = false;
    }
   // public System.Action<bool> detected;

    // ����Ƽ �����Ϳ� ��ä���� �׷��� �޼ҵ�
    private void OnDrawGizmos()
    {
        Handles.color = isCollision ? _red : _blue;
        // DrawSolidArc(������, ��ֺ���(��������), �׷��� ���� ����, ����, ������)
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, angleRange / 2, radius);
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -angleRange / 2, radius);
    }
}
   

