using monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearbyMonsterBehavior : MonoBehaviour
{
    private MonsterEvents monsterEvents;
    Monster monster;

    private void Start()
    {
        monster = Gamemanager.Inst.Monster;
        monsterEvents = FindObjectOfType<MonsterEvents>();
        monsterEvents.OnMonsterAttacked += ReactToMonsterAttack;
    }

    private void OnDisable()
    {
        monsterEvents.OnMonsterAttacked -= ReactToMonsterAttack;
    }

    private void ReactToMonsterAttack(Monster attackedMonster)
    {
        // 공격받은 몬스터와 일정 거리 이내의 몬스터들은 모여드는 반응
        float reactionDistance = 10f;
        float distanceToAttackedMonster = Vector3.Distance(transform.position, attackedMonster.transform.position);

        if (distanceToAttackedMonster <= reactionDistance)
        {
            monster.chaseState.EnterState();
        }
    }
}