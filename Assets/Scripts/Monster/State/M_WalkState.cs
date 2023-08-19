using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
            monster.onMove = true;
            SetMove();
            //areaMin = new Vector3(monster.spawner.spawnPosition.x - 5f, monster.spawner.spawnPosition.y, monster.spawner.spawnPosition.z - 5f);
            //areaMax = new Vector3(monster.spawner.spawnPosition.x + 5f, monster.spawner.spawnPosition.y, monster.spawner.spawnPosition.z + 5f);
            monster.nav.speed = 2; 
            monster.nav.angularSpeed = 200;
            
        }
        public void MoveLogic()
        {
           float distance = Vector3.Distance(monster.patrolTargetPosition, monster.transform.position);
            if(distance < 1f)
            {
                monster.idleState.EnterState();
            }

            //monster.moveDirection.y = 0;
            //monster.targetRotation = Quaternion.LookRotation(monster.moveDirection);
            //monster.transform.rotation = Quaternion.Slerp(monster.transform.rotation, monster.targetRotation, monster.rotationSpeed * Time.deltaTime);

            //if (monster.characterController.isGrounded == false)
            //{
            //    monster.moveDirection.y += monster.gravity * Time.fixedDeltaTime;
            //}

            //monster.characterController.Move(monster.dir * monster.speed * Time.fixedDeltaTime);


            //if (monster.transform.position.z > areaMax.z)
            //{
            //    SetMove();
            //}
            //else if (monster.transform.position.z < areaMin.z)
            //{
            //    SetMove();
            //}
            

        }

        public void SetMove()
        {
            areaMin = new Vector3(monster.SpawnPosition.x - 5f, monster.SpawnPosition.y, monster.SpawnPosition.z - 5f);
            areaMax = new Vector3(monster.SpawnPosition.x + 5f, monster.SpawnPosition.y, monster.SpawnPosition.z + 5f);

            float x;
            float z;

            x = Random.Range(areaMin.x, areaMax.x);
            z = Random.Range(areaMin.z, areaMax.z);
            //if (monster.transform.position.z > 5)
            //{
            //    z = areaMin.z;
            //}
            //else
            //{
            //    z = areaMax.z;
            //}
            monster.patrolTargetPosition = new Vector3(x, monster.transform.position.y, z);
            //monster.moveDirection = monster.targetPosition - monster.transform.position;
            //monster.dir.y = 0f;
            //monster.dir = monster.moveDirection.normalized;
            monster.nav.SetDestination(monster.patrolTargetPosition);

        }
    }
}

    
