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
        Monster monster;
      

        public M_MeleeAttackState(Monster monster)
        {
            
            this.monster = monster;
        }
        public void EnterState()
        {
            
            monster.monsterCurrentStates = this;
         
            monster.nav.ResetPath();
          
        }

        public void MoveLogic()
        {
            if(monster.attack_FOV.isCollision)
            {
                monster.MonsterAnimationChange(true);
                //
            }
           if(!monster.attack_FOV.isCollision ) 
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