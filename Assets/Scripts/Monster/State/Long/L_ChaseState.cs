using player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace L_monster

{
    public class L_ChaseState : MonsterState
    {
        L_Monster monster;
        L_State state = L_State.CHASE;

        int maxChaseDistance = 10;

        public L_ChaseState(L_Monster monster)
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

            if (spawnDistance <= maxChaseDistance)
            {
                monster.nav.SetDestination(monster.target.position);
                Debug.Log($"{monster.name} : 추적가즈아");
                if (monster.nav.remainingDistance <= monster.distance)
                {
                    monster.attack_Ready.EnterState();
                }
            }
            else
            {
                if (!monster.isback)
                {
                    monster.isback = true;
                    monster.backState.EnterState();
                    Debug.Log("도망준비");
                }
            }

        }

    }
}

