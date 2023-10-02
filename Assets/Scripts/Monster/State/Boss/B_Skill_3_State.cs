using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace boss

{
    public class B_Skill_3_State : MonsterState
    {
        Boss_Monster boss;
        B_State state = B_State.SKILL_3;
        public B_Skill_3_State(Boss_Monster boss)
        {
            this.boss = boss;
        }
        public void EnterState()
        {
            boss.bossCollider.enabled = false;
            boss.monsterCurrentStates = this;
            boss.MonsterAnimatorChange((int)state);
            Debug.Log("¹ßµ¿_2");
            boss.isSkillCooldown = true;
            boss.nav.ResetPath();
        }

        public void MoveLogic()
        {
           if(boss.Weapondive)
            {
                if (boss.skill_Weapon.transform.position.y < 3f)
                {
                    boss.Weapondive = false;
                }
                else
                {
                    boss.skill_Weapon.transform.Translate(Vector3.forward, Space.Self);
                }

            }
           if(boss.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                boss.bossCollider.enabled = true;
                boss.idleState.EnterState();
            }
          
        }

    }
}
