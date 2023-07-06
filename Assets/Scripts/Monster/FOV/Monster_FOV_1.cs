using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Monster_FOV_1 : MonoBehaviour
{
    
    public Transform target;    
    public float angleRange = 30f;
    public float radius = 3f;

    Color _blue = new Color(0f, 0f, 1f, 0.2f);
    Color _red = new Color(1f, 0f, 0f, 0.2f);

    public bool isCollision = false;

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
               
            }

            else
                isCollision = false;

        }
        else
            isCollision = false;
    }


  
    private void OnDrawGizmos()
    {
        Handles.color = isCollision ? _red : _blue;

        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, angleRange / 2, radius);
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -angleRange / 2, radius);
    }
}
