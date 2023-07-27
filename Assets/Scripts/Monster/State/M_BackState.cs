using player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace monster

{
    public class M_BackState : MonsterState
    {
        State state = State.BACK;
        Monster monster;
        public M_BackState(Monster monster)
        {
            this.monster = monster;
        }

        public void EnterState()
        {
            monster.monsterCurrentStates = this;
            monster.MonsterAnimatorChange((int)state);
           
            // monster.startpoint.position = monster.spawnPosition;
        }

        public void MoveLogic()
        {
            

            Vector3 direction = monster.spawnPosition - monster.transform.position;
            direction.y = 0;
        

            float distance = Vector3.Distance(monster.spawnPosition, monster.transform.position);
            if (distance > 1f)
            {
                direction = (monster.spawnPosition - monster.transform.position).normalized;


                if (monster.characterController.isGrounded == false)
                {
                    direction.y += monster.gravity * Time.fixedDeltaTime;
                }
                monster.nav.speed = 4;
                monster.nav.angularSpeed = 240;
                monster.nav.SetDestination(monster.spawnPosition);
                // monster.characterController.Move(direction * monster.speed * Time.fixedDeltaTime);
            }
            if (distance <= 1f)
            {

                monster.nav.ResetPath();
               monster.moveHelper();
               
            }
        }
    }
}

