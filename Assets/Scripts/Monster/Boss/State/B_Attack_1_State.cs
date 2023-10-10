using player;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace boss

{
    public class B_Attack_1_State : MonsterState
    {
        Boss_Monster boss;
        B_State state = B_State.ATTACK_1;
        float a;
        Vector3 targetPos;
        public B_Attack_1_State(Boss_Monster boss)
        {
            this.boss = boss;
        }
        public void EnterState()
        {
            targetPos = boss.target.position;
            
            boss.isAttack = true;
            boss.boss_CurrentStates = this;
            boss.MonsterAnimatorChange((int)state);
            boss.nav.ResetPath();
            a = Random.value;
        }

        public void MoveLogic()
        {
            Vector3 direction = targetPos - boss.transform.position;
            direction.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            Quaternion offsetRotation = Quaternion.Euler(0, 0, 0);

            targetRotation *= offsetRotation;

            boss.transform.rotation = Quaternion.Slerp(boss.transform.rotation, targetRotation, Time.deltaTime * boss.rotationSpeed);

            if (boss.animator.GetCurrentAnimatorStateInfo(0).IsName("Boss_Atk1") 
                && boss.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && boss.isAttack)
            {
                if(!boss.isAtkCooldown && boss.isSkillCooldown)
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
                    boss.isAttack = false;
                    boss.isAtkCooldown = true;
                    boss.idleState.EnterState();
                
            }
        }

    }
}
