using l_monster;
using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    L_Monster monster;
    PlayerController player;
    Vector3 pos;
    Vector3 dir;
    float speed;

    private void Awake()
    {
        monster = FindObjectOfType<L_Monster>();
        player = FindObjectOfType<PlayerController>();
        Transform child = player.transform.GetChild(0);
        pos = child.transform.position;
        speed = 3;
    }

    private void Start()
    {
        dir = (pos - monster.arrowShootPosition.transform.position).normalized;  
        transform.Rotate(Vector3.forward);
    }

    private void Update()
    {
        transform.Translate(dir * Time.deltaTime * speed);
    }
}
