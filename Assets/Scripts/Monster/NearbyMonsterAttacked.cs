using monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearbyMonsterAttacked : MonoBehaviour
{
    Monster_Base monster;

    private void Awake()
    {
        monster = FindObjectOfType<Monster_Base>();
    }

    /// <summary>
    /// 주변 몬스터가 공격당할시 반응을 위한 함수
    /// </summary>
    /// <param name="attackedMonster">공격당한 몬스터</param>
    public void ReactToMonsterAttack(Monster_Base attackedMonster)
    {
            if (!monster.isFriendsAttacked && attackedMonster != null)
            {
                monster.isFriendsAttacked = true;
                Debug.Log("친구가 공격받음");
                // 공격받은 몬스터와 일정 거리 이내의 몬스터들은 모여드는 반응
                float reactionDistance = 10f;
                float distanceToAttackedMonster = Vector3.Distance(transform.position, attackedMonster.transform.position);

                if (distanceToAttackedMonster <= reactionDistance)
                {
                    monster.Detect();
                    Debug.Log($"{monster.name} : 반응");
                    
                }
            }
    }  
    
}