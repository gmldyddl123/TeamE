using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace monster

{
    public class M_DetectedState : MonsterState
    {
        State state = State.DETECTED;
        Monster monster;
        public M_DetectedState(Monster monster)
        {
            this.monster = monster;
        }

        public void EnterState()
        {
            monster.nav.isStopped = true;
            monster.monsterCurrentStates = this;
            monster.MonsterAnimatorChange((int)state);
            monster.MonsterAnimationChange(false);
            monster.nav.ResetPath();
            Debug.Log("°¨Áö");
        }

        public void MoveLogic()
        {
            if (monster.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
               monster.chaseState.EnterState();   
            }
        }
    }
}


