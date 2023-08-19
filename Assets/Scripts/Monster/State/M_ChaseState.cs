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
        Monster monster;
        State state = State.CHASE;

        int maxChaseDistance = 10;
        public M_ChaseState(Monster monster)
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
            monster.nav.speed = 2;
            monster.nav.angularSpeed = 120;
            monster.nav.SetDestination(monster.target.position);
        }

        public void MoveLogic()
        {
            float spawnDistance = Vector3.Distance(monster.SpawnPosition, monster.transform.position);
            float distance = Vector3.Distance(monster.target.position, monster.transform.position);
            if (distance > monster.Distance)
            {
                monster.nav.SetDestination(monster.target.position);
            }
            if (distance <= monster.Distance) 
            {
                monster.Attack_Ready_M.EnterState();
            }
           
            if (spawnDistance > maxChaseDistance)
            {
                monster.Back();  
            }

        }

    }
}

