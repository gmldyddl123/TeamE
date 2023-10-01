using player;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace boss
{
    public class B_ChaseState : MonsterState
    {
        Boss_Monster boss;
        B_State state = B_State.CHASE;
        public B_ChaseState(Boss_Monster boss)
        {
            this.boss = boss;
        }
        public void EnterState()
        {
            boss.monsterCurrentStates = this;
            boss.MonsterAnimatorChange((int)state);
        }

        public void MoveLogic()
        {
           
          
        }

    }
}
