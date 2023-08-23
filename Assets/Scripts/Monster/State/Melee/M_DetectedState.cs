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
            Debug.Log("����Ʈ");

        }

        public void MoveLogic()
        {
            if (monster.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && monster.animator.GetCurrentAnimatorStateInfo(0).IsName("Detected"))
            {
                Debug.Log("�������� ����");
                monster.chaseState.EnterState();
            }

            //if (monster.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            //{

            //    Debug.Log("�������� ����");
            //    monster.chaseState.EnterState();
            //}
        }
    }
}


