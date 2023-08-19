using player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace monster

{
    public class M_BackState : MonsterState
    {
        State state = State.BACK;
        Monster monster;
        
        public M_BackState(Monster monster)
        {
            this.monster = monster;
        }

        public void EnterState()
        {
            // 과정 수정 필요
            monster.monsterCurrentStates = this;
            monster.MonsterAnimatorChange((int)0);
            monster.nav.isStopped = true;
            monster.onMove = false;
            monster.isAttack = false;
            monster.monsterEvents.OnMonsterAttacked -= monster.nearbyMonster.ReactToMonsterAttack;
            monster.nav.angularSpeed = 240;
            monster.nav.SetDestination(monster.SpawnPosition);
            monster.nav.isStopped = false;
            monster.MonsterAnimatorChange((int)state);
            monster.nav.speed = 4;

            // monster.startpoint.position = monster.spawnPosition;
        }

        public void MoveLogic()
        {
                

            float distance = Vector3.Distance(monster.SpawnPosition, monster.transform.position);
            
            if (distance <= 1f)
            {
                
                monster.idleState.EnterState();
               
            }
        }
    }
}

