using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace monster

{
    public class M_WalkState : MonsterState 
    {
        State state = State.WALK;
        Monster monster;

        bool patrolTarget = true;

        Vector3 areaMin;
        Vector3 areaMax;
        public M_WalkState(Monster monster)
        {
            this.monster = monster;
        }

        public void EnterState()
        {
            monster.monsterCurrentStates = this;
            patrolTarget = true;
            SetMove();       
            monster.onMove = true;
        }
        public void MoveLogic()
        {
            if(!patrolTarget)
            {
                if(monster.nav.remainingDistance < 1f)
                {
                    Debug.Log("도착");
                    monster.nav.ResetPath();
                    monster.idleState.EnterState();
                }
            }
        }

        public void SetMove()
        {
            CalculatePatrolArea();

            Vector3 patrolTargetPosition;
            bool foundValidTarget = false;

            // 시도 횟수 제한을 두어 무한 루프를 방지합니다.
            int maxAttempts = 10;
            int attempt = 0;

            while (!foundValidTarget && attempt < maxAttempts)
            {
                float x = Random.Range(areaMin.x, areaMax.x);
                float z = Random.Range(areaMin.z, areaMax.z);
                patrolTargetPosition = new Vector3(x, 0, z);

                // 몬스터의 현재 위치와 새로 생성된 좌표 사이의 거리를 계산합니다.
                float distanceToMonster = Vector3.Distance(monster.transform.position, patrolTargetPosition);

                // NavMesh 위에 이동 가능한 위치인지 확인합니다.
                if (NavMesh.SamplePosition(patrolTargetPosition, out NavMeshHit hit, 3f, NavMesh.AllAreas))
                {
                    // NavMesh 위의 유효한 위치이고, 일정 거리 이상 떨어져 있다면 이동합니다.
                    if (distanceToMonster >= 3) 
                    {
                        monster.nav.SetDestination(hit.position);
                        monster.MonsterAnimatorChange((int)state);
                        Debug.Log($"{hit.position}");
                        foundValidTarget = true;
                    }
                }

                attempt++;
            }

            patrolTarget = false;
        }

        private void CalculatePatrolArea()
        {
            areaMin = new Vector3(monster.SpawnPosition.x - 5f, 0, monster.SpawnPosition.z - 5f);
            areaMax = new Vector3(monster.SpawnPosition.x + 5f, 0, monster.SpawnPosition.z + 5f);
        }
        //{
        //    areaMin = new Vector3(monster.SpawnPosition.x - 5f, 0, monster.SpawnPosition.z - 5f);
        //    areaMax = new Vector3(monster.SpawnPosition.x + 5f, 0, monster.SpawnPosition.z + 5f);

        //    float x;
        //    float z;

        //    x = Random.Range(areaMin.x, areaMax.x);
        //    z = Random.Range(areaMin.z, areaMax.z);


        //    Vector3 patrolTargetPosition = new Vector3(x, 0, z);
        //    monster.nav.SetDestination(patrolTargetPosition);
        //    patrolTarget = false;


        //}
    }
}

    
