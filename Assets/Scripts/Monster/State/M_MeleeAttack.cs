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
        float rotationSpeed;
        
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
            rotationSpeed = monster.rotationSpeed;
        }

        public void MoveLogic()
        {
            Vector3 direction = monster.target.position - monster.transform.position;
            direction.y = 0;
            if(monster.animatorAttack)
            {
                direction = Vector3.forward;
                monster.targetRotation = Quaternion.LookRotation(direction);
                monster.transform.rotation = Quaternion.Slerp(monster.transform.rotation, monster.targetRotation, rotationSpeed * Time.deltaTime);
                Debug.Log($"{rotationSpeed}");
            }
            if (!monster.animatorAttack)
            {
                rotationSpeed = monster.rotationSpeed;
                monster.targetRotation = Quaternion.LookRotation(direction);
                monster.transform.rotation = Quaternion.Slerp(monster.transform.rotation, monster.targetRotation, rotationSpeed * Time.deltaTime);
               
               
                if (monster.attack_FOV.isCollision )
                {
                    monster.PlayerAnimationChamge(true);
                }
                if(!monster.attack_FOV.isCollision && monster.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) 
                {
                    monster.PlayerAnimationChamge(false);
                }
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