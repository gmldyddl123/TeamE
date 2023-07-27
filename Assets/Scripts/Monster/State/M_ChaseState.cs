using player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace monster

{
    public class M_ChaseState : MonsterState
    {
        Monster monster;
        State state = State.CHASE;
        public M_ChaseState(Monster monster)
        {
            this.monster = monster;
        }

        public void EnterState()
        {
            monster.monsterCurrentStates = this;
            monster.MonsterAnimatorChange((int)state);
          
            monster.nav.speed = 2;
            monster.nav.angularSpeed = 120;
        }

        public void MoveLogic()
        {
            monster.isAttack = false;

            float distance = Vector3.Distance(monster.target.position, monster.transform.position);
            if (distance > monster.Distance && !monster.isAttack)
            {
                monster.nav.SetDestination(monster.target.position);
            }
            if(distance <= monster.Distance) 
            {
                monster.isAttack = true;
                monster.Attack_Ready_M.EnterState();
            }
           
        }

    }
}

