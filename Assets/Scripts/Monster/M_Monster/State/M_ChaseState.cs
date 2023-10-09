using player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace monster

{
    public class M_ChaseState : MonsterState
    {
        M_Monster monster;
        M_State state = M_State.CHASE;

        int maxChaseDistance = 10;
       
        public M_ChaseState(M_Monster monster)
        {
            this.monster = monster;
        }

        public void EnterState()
        {
            monster.monsterCurrentStates = this;
            monster.MonsterAnimatorChange((int)state);
            monster.onMove = false;
            monster.isAttack = false;
            monster.nav.ResetPath();
            monster.nav.SetDestination(monster.target.position);
            monster.nav.speed = monster.chaseSpeed;
        }

        public void MoveLogic()
        {
            float spawnDistance = Vector3.Distance(monster.SpawnPosition, monster.transform.position);
          
           if(spawnDistance <= maxChaseDistance)
            {
                monster.nav.SetDestination(monster.target.position);
                Debug.Log($"{monster.name} : ���������");
                if (monster.nav.remainingDistance <= monster.distance)
                {
                    monster.Attack_Ready_M.EnterState();
                }
            }
           else
           { 
                if(!monster.isback) 
                {
                    monster.isback = true;
                    monster.backState.EnterState();
                    Debug.Log("�����غ�");
                }
            }

        }

    }
}
