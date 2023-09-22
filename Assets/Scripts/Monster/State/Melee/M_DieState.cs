using UnityEngine;

namespace monster

{
    public class M_DieState : MonsterState
    {

        M_Monster monster;
        //State state = State.DIE;

        public M_DieState(M_Monster monster)
        {

            this.monster = monster;
        }
        public void EnterState()
        {
            monster.monsterCurrentStates = this;
            monster.MonsterDieChange(true);
            monster.Die();
        }

        public void MoveLogic()
        {
            if (monster.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && monster.animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
            {
                monster.AfterDie();
            }
        }
    }

}
