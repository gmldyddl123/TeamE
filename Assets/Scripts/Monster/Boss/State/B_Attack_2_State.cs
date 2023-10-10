using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace boss

{
    public class B_Attack_2_State : MonsterState
    {
        Boss_Monster boss;
        B_State state = B_State.ATTACK_2;
        Vector3 targetPos;
        float a;
        public B_Attack_2_State(Boss_Monster boss)
        {
            this.boss = boss;
        }
        public void EnterState()
        {
            boss.MonsterAnimatorChange((int)state);
            targetPos = boss.target.transform.position;
            
            boss.isAttack = true;
            boss.boss_CurrentStates = this;
            boss.nav.ResetPath();
            a = Random.value;
        }

        public void MoveLogic()
        {
            Vector3 direction = targetPos - boss.transform.position;
            direction.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            Quaternion offsetRotation = Quaternion.Euler(0, 10, 0);

            targetRotation *= offsetRotation;

            boss.transform.rotation = Quaternion.Slerp(boss.transform.rotation, targetRotation, Time.deltaTime * boss.rotationSpeed);

            if (boss.animator.GetCurrentAnimatorStateInfo(0).IsName("Boss_Atk2") 
                && boss.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && boss.isAttack)
            {
                if (!boss.isAtkCooldown && boss.isSkillCooldown)
                {
                    if (a <= 0.5)
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
                Debug.Log("atk_2 °ø°Ý³¡");

            }
        }

    }
}
