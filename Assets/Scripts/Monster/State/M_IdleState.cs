using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace monster

{
    public class M_IdleState : MonsterState
    {
        Monster monster;
        State state = State.IDLE;
        public M_IdleState(Monster monsterTEST)
        {
            this.monster = monsterTEST;
        }
        public void EnterState()
        {
            monster.monsterCurrentStates = this;
            monster.MonsterAnimatorChange((int)state);
            monster.onMove = true;
            monster.monsterEvents.OnMonsterAttacked += monster.nearbyMonster.ReactToMonsterAttack;

        }

        public void MoveLogic()
        {
            monster.Patrol();
        }

    }
}