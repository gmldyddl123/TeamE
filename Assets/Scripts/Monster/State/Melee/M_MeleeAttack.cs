using player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
namespace monster

{
    public class M_MeleeAttackState : MonsterState
    {
        M_Monster monster;
        M_State state = M_State.ATTACK;

        public M_MeleeAttackState(M_Monster monster)
        {
            
            this.monster = monster;
        }
        public void EnterState()
        {
           
            monster.monsterCurrentStates = this;
            monster.MonsterAnimatorChange((int)state);
            monster.isAttack = true;
            monster.nav.ResetPath();
        }

        public void MoveLogic()
        {
            if(monster.attack_FOV != null)
            {
                if (monster.attack_FOV.isCollision && monster.isAttack)
                {
                    monster.MonsterAnimationChange(true);

                }
                if (!monster.attack_FOV.isCollision)
                {

                    if (monster.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                    {
                        monster.MonsterAnimationChange(false);
                        monster.Attack_Ready_M.EnterState();
                    }
                }
            }
            
        }

       
    }
}