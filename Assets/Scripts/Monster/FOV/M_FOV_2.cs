using player;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class M_FOV_2 : MonoBehaviour
{
    PlayerController player;
   Transform target;    
    public float angleRange = 30f;
    public float radius = 3f;
    bool hasDetected;

    Color _blue = new Color(0f, 0f, 1f, 0.2f);
    Color _red = new Color(1f, 0f, 0f, 0.2f);

    public bool isCollision = false;
    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        target = player.transform;

    }
    void Update()
    {
        Vector3 interV = target.position - transform.position;


        if (interV.magnitude < radius)
        {

            float dot = Vector3.Dot(interV.normalized, transform.forward);

            float theta = Mathf.Acos(dot);

            float degree = Mathf.Rad2Deg * theta;


            if (degree <= angleRange * 0.5f)
            {
                isCollision = true;
                if (!hasDetected)
                {
                    hasDetected = true;
                    detected_2?.Invoke();
                }
            }

            else
            {
                isCollision = false;
                hasDetected = false;
            }

        }
        else
        {
            isCollision = false;
            hasDetected = false;
        }
    }
    public System.Action detected_2;


    private void OnDrawGizmos()
    {
        Handles.color = isCollision ? _red : _blue;

        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, angleRange / 2, radius);
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -angleRange / 2, radius);
    }
}
