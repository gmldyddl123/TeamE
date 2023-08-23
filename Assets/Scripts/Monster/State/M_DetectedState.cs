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
            monster.nav.ResetPath();
            monster.nav.isStopped = true;
            monster.monsterCurrentStates = this;
            monster.MonsterAnimatorChange((int)state);
            //monster.MonsterAnimationChange(false);
            Debug.Log("디텍트");

        }

        public void MoveLogic()
        {
            if (monster.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && monster.animator.GetCurrentAnimatorStateInfo(0).IsName("Detected"))
            {
                Debug.Log("추적으로 변경");
                monster.chaseState.EnterState();
            }

            //if (monster.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            //{

            //    Debug.Log("추적으로 변경");
            //    monster.chaseState.EnterState();
            //}
        }
    }
}


