using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace monster

{
    public class M_WalkState : MonsterState 
    {
        State state = State.WALK;
        Monster monster;
        Vector3 areaMin = new Vector3(-7.5f, 0, 2.5f);
        Vector3 areaMax = new Vector3(-2.5f, 0, 7.5f);
        public M_WalkState(Monster monsterTEST)
        {
            this.monster = monsterTEST;
        }

        public void EnterState()
        {
            monster.monsterCurrentStates = this;
            monster.MonsterAnimatorChange((int)state);
            SetMove();
        }

        public void MoveLogic()
        {
           

            monster.moveDirection.y = 0;
            monster.targetRotation = Quaternion.LookRotation(monster.moveDirection);
            monster.transform.rotation = Quaternion.Slerp(monster.transform.rotation, monster.targetRotation, monster.rotationSpeed * Time.deltaTime);

            if (monster.characterController.isGrounded == false)
            {
                monster.moveDirection.y += monster.gravity * Time.fixedDeltaTime;
            }

            monster.characterController.Move(monster.dir * monster.speed * Time.fixedDeltaTime);


            if (monster.transform.position.z > areaMax.z)
            {
                SetMove();
            }
            else if (monster.transform.position.z < areaMin.z)
            {
                SetMove();
            }

        }

        public void SetMove()
        {
            float x;
            float z;

            x = Random.Range(areaMin.x, areaMax.x);
            if (monster.transform.position.z > 5)
            {
                z = areaMin.z;
            }
            else
            {
                z = areaMax.z;
            }
            monster.targetPosition = new Vector3(x, 0, z);
            monster.moveDirection = monster.targetPosition - monster.transform.position;
            monster.dir.y = 0f;
            monster.dir = monster.moveDirection.normalized;

        }
    }
}

    
