using l_monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    L_Monster monster;
    Vector3 pos;
    Vector3 dir;
    float speed;

    private void Awake()
    {
        monster = FindObjectOfType<L_Monster>();
        speed = 3;
    }

    private void Start()
    {
        pos = monster.target.position;
        dir = (pos - transform.position).normalized;
        
    }

    private void Update()
    {
        transform.Translate(dir * Time.deltaTime * speed);
    }
}
