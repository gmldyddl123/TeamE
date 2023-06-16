using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace monster

{
    public class M_ChaseState : MonsterState
    {

        MonsterTEST monster;
        State state = State.CHASE;

        public M_ChaseState(MonsterTEST monsterTEST)
        {
            this.monster = monsterTEST;
        }

        public void EnterState()
        {
            monster.monsterCurrentStates = this;
            monster.PlayerAnimoatrChage((int)state);
        }

        public void MoveLogic()
        {
            Vector3 direction = monster.target.position - monster.transform.position;
            direction.y = 0;
            monster.targetRotation = Quaternion.LookRotation(direction);
            monster.transform.rotation = Quaternion.Slerp(monster.transform.rotation, monster.targetRotation, monster.rotationSpeed * Time.deltaTime);

            float distance = Vector3.Distance(monster.target.position, monster.transform.position);
            if (distance > monster.Distance)
            {
                direction = (monster.target.position - monster.transform.position).normalized;
                //direction = new Vector3(monster.target.position.x - monster.transform.position.x,
                //    monster.direction.y,
                //    monster.target.position.z - monster.transform.position.z).normalized;

                if (monster.characterController.isGrounded == false)
                {
                    direction.y += monster.gravity * Time.fixedDeltaTime;
                }


                monster.characterController.Move(direction * monster.speed * Time.fixedDeltaTime);

            }
           
        }

    }
}
