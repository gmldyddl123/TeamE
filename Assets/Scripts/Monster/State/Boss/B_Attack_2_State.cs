using player;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace boss

{
    public class B_Attack_2_State : MonsterState
    {
        Boss_Monster boss;
        B_State state = B_State.ATTACK_2;
        public B_Attack_2_State(Boss_Monster boss)
        {
            this.boss = boss;
        }
        public void EnterState()
        {
            boss.isAttack = true;
            boss.monsterCurrentStates = this;
            boss.MonsterAnimatorChange((int)state);
            boss.nav.ResetPath();
        }

        public void MoveLogic()
        {
            if (boss.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                boss.isAtkCooldown = true;
                boss.idleState.EnterState();
            }
        }

    }
}
