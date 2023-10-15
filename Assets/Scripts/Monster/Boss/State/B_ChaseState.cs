using player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace boss
{
    public class B_ChaseState : MonsterState
    {
        float a;
        float b;
        Boss_Monster boss;
        B_State state = B_State.CHASE;
        public B_ChaseState(Boss_Monster boss)
        {
            this.boss = boss;
        }
        public void EnterState()
        {
                Debug.Log("체이스스테이트 입장완");
                Debug.Log(boss.isSkillCooldown);
                boss.boss_CurrentStates = this;
                boss.MonsterAnimatorChange((int)state);
                boss.nav.SetDestination(boss.target.position);
                a = Random.value;
                b = Random.value;
        }

        public void MoveLogic()
        {
            

            if(boss.HP <= (boss.MaxHP * 0.5f) && !boss.Phaze_2)
            {
                Debug.Log("2페");
                boss.skill_2_State.EnterState();
                boss.Phaze_2 = true;
            }
            // 스킬보다 공격먼저 우선순위
            if (boss.FOV1.isCollision 
                && !boss.isAtkCooldown)
            {
                if(a <= 0.5)
                {
                    boss.attack_1_State.EnterState();   //위 아래 공격
                    Debug.Log("공격1");
                }
                else
                {
                    boss.attack_2_State.EnterState();   //옆 공격
                    Debug.Log("공격2");
                }
            }
            else if (boss.FOV2.isCollision 
                     && !boss.isSkillCooldown)
            {
                if(b <= 0.5)
                {
                    boss.skill_1_State.EnterState();    //스킬 1 발동
                    Debug.Log("스킬1");
                }
                else
                {
                    boss.skill_3_State.EnterState();    //스킬 2 발동
                    Debug.Log("스킬2");
                }
            }

            boss.nav.SetDestination(boss.target.position);
        }
            

        

    }
}
