using player;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace boss

{
    public class B_DieState : MonsterState
    {
        Boss_Monster boss;
        //B_State state = B_State.DIE;
        public B_DieState(Boss_Monster boss)
        {
            this.boss = boss;
        }
        public void EnterState()
        {
            boss.boss_CurrentStates = this;
            boss.MonsterTriggerChange("Die");
            boss.Die();
        }

        public void MoveLogic()
        {
            if (boss.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && boss.animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
            {
                boss.AfterDie();
            }

        }

    }
}
