using player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace monster

{
    public class M_ChaseState : MonsterState
    {
        Vector3 dir;
        Monster monster;
        State state = State.CHASE;
        public M_ChaseState(Monster monster)
        {
            this.monster = monster;
        }

        public void EnterState()
        {
            monster.monsterCurrentStates = this;
            monster.PlayerAnimoatrChage((int)state);
            monster.nav.speed = 2;
            monster.nav.angularSpeed = 120;
        }

        public void MoveLogic()
        {
            monster.isAttack = false;

            Vector3 direction = monster.target.position - monster.transform.position;
            direction.y = 0;
            //monster.targetRotation = Quaternion.LookRotation(direction);
            //monster.transform.rotation = Quaternion.Slerp(monster.transform.rotation, monster.targetRotation, monster.rotationSpeed * Time.deltaTime);

            float distance = Vector3.Distance(monster.target.position, monster.transform.position);
            if (distance > monster.Distance && !monster.isAttack)
            {
                dir = monster.target.position - monster.transform.position;
                dir.y = 0;
                direction = dir.normalized;

                if (monster.characterController.isGrounded == false)
                {
                    direction.y += monster.gravity * Time.fixedDeltaTime;
                }

                monster.nav.SetDestination(monster.endpoint.position);
                //monster.characterController.Move(direction * monster.speed * Time.fixedDeltaTime);

            }
            if(distance <= monster.Distance) 
            {
                monster.isAttack = true;
                monster.melee_AttackState.EnterState();
            }
           
        }

    }
}

