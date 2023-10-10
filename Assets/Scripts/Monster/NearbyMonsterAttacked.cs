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
    /// �ֺ� ���Ͱ� ���ݴ��ҽ� ������ ���� �Լ�
    /// </summary>
    /// <param name="attackedMonster">���ݴ��� ����</param>
    public void ReactToMonsterAttack(Monster_Base attackedMonster)
    {
            if (!monster.isFriendsAttacked && attackedMonster != null)
            {
                monster.isFriendsAttacked = true;
                Debug.Log("ģ���� ���ݹ���");
                // ���ݹ��� ���Ϳ� ���� �Ÿ� �̳��� ���͵��� �𿩵�� ����
                float reactionDistance = 10f;
                float distanceToAttackedMonster = Vector3.Distance(transform.position, attackedMonster.transform.position);

                if (distanceToAttackedMonster <= reactionDistance)
                {
                    monster.Detect();
                    Debug.Log($"{monster.name} : ����");
                    
                }
            }
    }  
    
}