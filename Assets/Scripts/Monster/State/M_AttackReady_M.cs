using player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
namespace monster

{
    public class M_AttackReady_M : MonsterState
    {
        Monster monster;
       

        State state = State.IDLE;




        public M_AttackReady_M(Monster monster)
        {
            
            this.monster = monster;
        }
        public void EnterState()
        {
            monster.PlayerAnimoatrChage((int)state);
            monster.nav.ResetPath();
            monster.monsterCurrentStates = this;
        }

        public void MoveLogic()
        {
            if(monster.attack_FOV.isCollision)
            {
                monster.melee_AttackState.EnterState();
            }
            if(monster.attack_FOV.isCollision == false && monster.FOV2.isCollision && monster.animatorAttack ==false )
            {
                if (monster.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    Vector3 direction = monster.target.position - monster.transform.position;
                    direction.y = 0;
                    monster.targetRotation = Quaternion.LookRotation(direction);
                    monster.transform.rotation = Quaternion.Slerp(monster.transform.rotation, monster.targetRotation, monster.rotationSpeed * Time.deltaTime);
                }
            }
            float distance = Vector3.Distance(monster.target.position, monster.transform.position);
            if (distance > monster.Distance && monster.isAttack)
            {
                

                    monster.isAttack = false;
                    monster.chaseState.EnterState();
                
            }
        }

     
    }
}