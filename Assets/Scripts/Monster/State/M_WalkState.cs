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
        Vector3 areaMin;
        Vector3 areaMax;
        public M_WalkState(Monster monsterTEST)
        {
            this.monster = monsterTEST;
        }

        public void EnterState()
        {
            monster.monsterCurrentStates = this;
            monster.MonsterAnimatorChange((int)state);
            SetMove();
            areaMin = new Vector3(monster.spawnPosition.x - 2.5f, monster.spawnPosition.y, monster.spawnPosition.z - 2.5f);
            areaMax = new Vector3(monster.spawnPosition.x + 2.5f, monster.spawnPosition.y, monster.spawnPosition.z + 2.5f);
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

    
