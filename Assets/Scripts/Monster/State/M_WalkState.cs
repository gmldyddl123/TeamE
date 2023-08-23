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
                    Debug.Log("����");
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

            // �õ� Ƚ�� ������ �ξ� ���� ������ �����մϴ�.
            int maxAttempts = 10;
            int attempt = 0;

            while (!foundValidTarget && attempt < maxAttempts)
            {
                float x = Random.Range(areaMin.x, areaMax.x);
                float z = Random.Range(areaMin.z, areaMax.z);
                patrolTargetPosition = new Vector3(x, 0, z);

                // ������ ���� ��ġ�� ���� ������ ��ǥ ������ �Ÿ��� ����մϴ�.
                float distanceToMonster = Vector3.Distance(monster.transform.position, patrolTargetPosition);

                // NavMesh ���� �̵� ������ ��ġ���� Ȯ���մϴ�.
                if (NavMesh.SamplePosition(patrolTargetPosition, out NavMeshHit hit, 3f, NavMesh.AllAreas))
                {
                    // NavMesh ���� ��ȿ�� ��ġ�̰�, ���� �Ÿ� �̻� ������ �ִٸ� �̵��մϴ�.
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

    
