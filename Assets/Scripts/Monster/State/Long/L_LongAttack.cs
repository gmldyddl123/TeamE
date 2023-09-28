using UnityEngine;

namespace l_monster

{
    public class L_LongAttackState : MonsterState
    {

        L_State state = L_State.ATTACK;
        L_Monster monster;
        
        
        public L_LongAttackState(L_Monster monster)
        {
            this.monster = monster;
        }

        public void EnterState()
        {
            monster.monsterCurrentStates = this;
            monster.MonsterAnimationChange(true);

        }

        public void MoveLogic()
        {

                if(monster.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    monster.attack_Ready.EnterState();
                }
            
            
        }

    }
}