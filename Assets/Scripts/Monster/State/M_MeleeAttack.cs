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
        
   //State state = State.MELEE_ATTACK;
        


        
        public M_MeleeAttackState(Monster monster)
        {
            
            this.monster = monster;
        }
        public void EnterState()
        {
            
            monster.monsterCurrentStates = this;
            //monster.PlayerAnimoatrChage((int)state);
            //monster.PlayerAnimationChamge(true);
            monster.nav.ResetPath();
        }

        public void MoveLogic()
        {
            if (!monster.animatorAttack)
            { 
                Vector3 direction = monster.target.position - monster.transform.position;
                direction.y = 0;
                monster.targetRotation = Quaternion.LookRotation(direction);
                monster.transform.rotation = Quaternion.Slerp(monster.transform.rotation, monster.targetRotation, monster.rotationSpeed * Time.deltaTime);
                if (monster.attack_FOV.isCollision )
                {
                       Vector3 dir = direction;
                        dir = Vector3.forward;
                    monster.PlayerAnimationChamge(true);
                }
                if(!monster.attack_FOV.isCollision && monster.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) 
                {
                        Vector3 dir = direction;
                        dir = Vector3.forward;
                        monster.PlayerAnimationChamge(false);
                }
            }
            if(monster.animatorAttack)
            {
                Vector3 direction = Vector3.forward;
                direction.y = 0;
                monster.targetRotation = Quaternion.LookRotation(direction);
               // monster.transform.rotation = Quaternion.Slerp(monster.transform.rotation, monster.targetRotation, monster.rotationSpeed * Time.deltaTime);
            }

                float distance = Vector3.Distance(monster.target.position, monster.transform.position);
                if (distance > monster.Distance && monster.isAttack)
                {
              
                    if (monster.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                    {
                        monster.isAttack = false;
                        monster.chaseState.EnterState();
                    }
               
                }
        }

     
    }
}