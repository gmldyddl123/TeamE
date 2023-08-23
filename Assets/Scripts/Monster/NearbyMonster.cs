using monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearbyMonster : MonoBehaviour
{

    Monster monster;

   void Awake()
    {
        monster = GetComponent<Monster>();
    }

    

    public void ReactToMonsterAttack(Monster attackedMonster)
    {
        if (attackedMonster != null)
        {
            // ���ݹ��� ���Ϳ� ���� �Ÿ� �̳��� ���͵��� �𿩵�� ����
            float reactionDistance = 10f;
            float distanceToAttackedMonster = Vector3.Distance(transform.position, attackedMonster.transform.position);

            if (distanceToAttackedMonster <= reactionDistance)
            {
                monster.onMove = false;

                monster.detectedState.EnterState();
            }
        }
         
        
    }
}