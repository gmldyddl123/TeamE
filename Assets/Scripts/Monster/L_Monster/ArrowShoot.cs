using l_monster;
using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShoot : MonoBehaviour
{
    DisappearArrow disappearArrow;
    PlayerController player;
    L_Monster monster;
    Transform target;
    Transform arrowShootPosition;

    public GameObject arrow;

    private void Awake()
    {
        disappearArrow = FindObjectOfType<DisappearArrow>();
        player = FindObjectOfType<PlayerController>();
        monster = FindObjectOfType<L_Monster>();
        Transform child = player.transform.GetChild(0);
        target = child.transform;
        child = monster.transform.GetChild(3);
        arrowShootPosition = child.transform;


    }
    public void ArrowShooting()
    {
        Quaternion arrowRotation = monster.transform.rotation;
        Instantiate(arrow, arrowShootPosition.position, arrowRotation);
    }
}
