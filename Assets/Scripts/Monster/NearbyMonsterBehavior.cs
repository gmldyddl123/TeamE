using monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearbyMonsterBehavior : Monster
{
    


    private void Start()
    {
     monsterEvents.OnMonsterAttacked += ReactToMonsterAttack;
    }

    

    private void ReactToMonsterAttack(Monster attackedMonster)
    {
        if (attackedMonster != null)
        {
            // ���ݹ��� ���Ϳ� ���� �Ÿ� �̳��� ���͵��� �𿩵�� ����
            float reactionDistance = 10f;
            float distanceToAttackedMonster = Vector3.Distance(transform.position, attackedMonster.transform.position);

            if (distanceToAttackedMonster <= reactionDistance)
            {
                chaseState.EnterState();
            }
        }
    }
}