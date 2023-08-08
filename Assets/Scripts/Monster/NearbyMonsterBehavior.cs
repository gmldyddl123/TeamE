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
        // ���ݹ��� ���Ϳ� ���� �Ÿ� �̳��� ���͵��� �𿩵�� ����
        float reactionDistance = 10f;
        float distanceToAttackedMonster = Vector3.Distance(transform.position, attackedMonster.transform.position);

        if (distanceToAttackedMonster <= reactionDistance)
        {
            monster.chaseState.EnterState();
        }
    }
}