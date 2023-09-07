using UnityEngine;

namespace L_monster

{
    public class L_DieState : MonsterState
    {

        L_Monster monster;
        //State state = State.DIE;

        public L_DieState(L_Monster monster)
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
