using monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearbyMonsterAttacked : MonoBehaviour
{

    Monster monster;
    

   void Awake()
    {
        monster = GetComponent<Monster>();
       
    }

   
    public void ReactToMonsterAttack(Monster attackedMonster)
    {
   
        //!monster.isFriendsAttacked &&
            if (!monster.isFriendsAttacked && attackedMonster != null)
            {
                 monster.isFriendsAttacked = true;
            Debug.Log("ģ���� ���ݹ���");
                // ���ݹ��� ���Ϳ� ���� �Ÿ� �̳��� ���͵��� �𿩵�� ����
                float reactionDistance = 10f;
                float distanceToAttackedMonster = Vector3.Distance(transform.position, attackedMonster.transform.position);

                if (distanceToAttackedMonster <= reactionDistance)
                {
                    monster.detectedState.EnterState();
                    Debug.Log($"{monster.name} : ����");
                    
                }
            }
    }  
    
}