using monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearbyMonster : Monster
{
    


    //private void Start()
    //{
    // //monsterEvents.OnMonsterAttacked += ReactToMonsterAttack;
    //}

    

    public void ReactToMonsterAttack(Monster attackedMonster)
    {
        if (attackedMonster != null)
        {
            // ���ݹ��� ���Ϳ� ���� �Ÿ� �̳��� ���͵��� �𿩵�� ����
            float reactionDistance = 10f;
            float distanceToAttackedMonster = Vector3.Distance(transform.position, attackedMonster.transform.position);

            if (distanceToAttackedMonster <= reactionDistance)
            {
                onMove = false;

                chaseState.EnterState();
            }
        }
         
        
    }
}