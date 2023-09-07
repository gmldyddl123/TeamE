using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace L_monster

{
    public class L_DetectedState : MonsterState
    {
        L_State state = L_State.DETECTED;
        L_Monster monster;
        public L_DetectedState(L_Monster monster)
        {
            this.monster = monster;
        }

        public void EnterState()
        {
            monster.onMove = false;
            monster.nav.ResetPath();
            monster.nav.isStopped = true;
            monster.monsterCurrentStates = this;
            monster.MonsterAnimatorChange((int)state);
            //monster.MonsterAnimationChange(false);
            Debug.Log($"{monster.name} : 디텍트");

        }

        public void MoveLogic()
        {
            if (monster.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && monster.animator.GetCurrentAnimatorStateInfo(0).IsName("Detected"))
            {
                Debug.Log($"{monster.name} : 추적으로 변경");
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


