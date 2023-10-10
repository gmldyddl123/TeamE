using player;
using System.Collections;
using System.Collections.Generic;
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
            boss.nav.SetDestination(boss.target.position);

            if (boss.FOV1.isCollision 
                && !boss.isAtkCooldown)
            {
                if(a <= 0.5)
                {
                    boss.attack_1_State.EnterState();
                }
                else
                {
                    boss.attack_2_State.EnterState();
                }
            }
            else if (boss.FOV2.isCollision 
                     && !boss.isSkillCooldown)
            {
                if(b <= 0.5)
                {
                    boss.skill_1_State.EnterState();
                    boss.coolReset = true;
                }
                else
                {
                    boss.skill_3_State.EnterState();
                    boss.coolReset = true;
                }
            }
        }
            
            

        

    }
}
