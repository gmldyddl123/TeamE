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
       

        State state = State.ATTACKREADY_M;




        public M_AttackReady_M(Monster monster)
        {
            
            this.monster = monster;
        }
        public void EnterState()
        {
            monster.nav.ResetPath();
            monster.monsterCurrentStates = this;
            monster.isAttack = false;
            monster.MonsterAnimatorChange((int)state);
        }

        public void MoveLogic()
        {
            float distance = Vector3.Distance(monster.target.position, monster.transform.position);
            if(monster.attack_FOV.isCollision)
            {
                monster.melee_AttackState.EnterState();
            }
            if(monster.attack_FOV.isCollision == false && (monster.FOV2.isCollision || monster.FOV1.isCollision) && monster.animatorAttack ==false )
            {
                    Vector3 direction = monster.target.position - monster.transform.position;
                    direction.y = 0;
                    monster.targetRotation = Quaternion.LookRotation(direction);
                    monster.transform.rotation = Quaternion.Slerp(monster.transform.rotation, monster.targetRotation, monster.rotationSpeed * Time.deltaTime);
            }
            if (distance > monster.Distance)
            {
                    monster.chaseState.EnterState();  
            }
        }

     
    }
}