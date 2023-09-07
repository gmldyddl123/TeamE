using UnityEngine;

namespace L_monster

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
            monster.MonsterAnimatorChange((int)state);

        }

        public void MoveLogic()
        {
            
            if (monster.attack_FOV.isCollision ==false) 
            {
                
                if(monster.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                { 
                    monster.chaseState.EnterState();
                }
            }
            
        }

    }
}