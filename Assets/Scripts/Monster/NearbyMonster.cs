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
            // 공격받은 몬스터와 일정 거리 이내의 몬스터들은 모여드는 반응
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