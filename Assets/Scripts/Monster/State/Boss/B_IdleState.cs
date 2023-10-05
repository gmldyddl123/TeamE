using UnityEngine;
using static UnityEditor.PlayerSettings;

namespace boss

{
    public class B_IdleState : MonsterState
    {
        Vector3 targetPos;   
        Boss_Monster boss;
        B_State state = B_State.IDLE;
        public B_IdleState(Boss_Monster boss)
        {
            this.boss = boss;
        }
        public void EnterState()
        {
            boss.MonsterAnimatorChange((int)state);
            targetPos = boss.target.position;
            boss.monsterCurrentStates = this;
            boss.nav.ResetPath();   
        }

        public void MoveLogic()
        {

            //Vector3 direction = targetPos - boss.transform.position;
            //direction.y = 0;
            //Quaternion targetRotation = Quaternion.LookRotation(direction);

            //// Y축으로 -30도 회전한 각도 생성
            //Quaternion offsetRotation = Quaternion.Euler(0, 30, 0);

            //// 원래 회전에 offsetRotation을 더합니다.
            //targetRotation *= offsetRotation;

            //boss.transform.rotation = Quaternion.Slerp(boss.transform.rotation, targetRotation, Time.deltaTime * boss.rotationSpeed);

            if (!boss.isAtkCooldown || !boss.isSkillCooldown)
            {
                Debug.Log("추적스테이트 입장전");
                boss.chaseState.EnterState();
            }
            else
            {
                Vector3 direction = targetPos - boss.transform.position;
                direction.y = 0;
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // Y축으로 -30도 회전한 각도 생성
                Quaternion offsetRotation = Quaternion.Euler(0, 30, 0);

                // 원래 회전에 offsetRotation을 더합니다.
                targetRotation *= offsetRotation;

                boss.transform.rotation = Quaternion.Slerp(boss.transform.rotation, targetRotation, Time.deltaTime * boss.rotationSpeed);
            }
        }

    }
}

