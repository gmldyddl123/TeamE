using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace boss

{
    public class B_Skill_1_State : MonsterState
    {
        Boss_Monster boss;
        Vector3 targetPos;

        B_State state = B_State.SKILL_1;
        public B_Skill_1_State(Boss_Monster boss)
        {
            this.boss = boss;
        }
        public void EnterState()
        {
            targetPos = boss.target.position;

            boss.isSkill = true;
            boss.nav.ResetPath();
            boss.isSkillCooldown = true;
            boss.monsterCurrentStates = this;
            boss.MonsterAnimatorChange((int)state);
            Debug.Log("¹ßµ¿_1");
        }

        public void MoveLogic()
        {

            Vector3 direction = targetPos - boss.transform.position;
            direction.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            Quaternion offsetRotation = Quaternion.Euler(0, 45, 0);

            targetRotation *= offsetRotation;

            boss.transform.rotation = Quaternion.Slerp(boss.transform.rotation, targetRotation, Time.deltaTime * boss.rotationSpeed);

            if (boss.animator.GetCurrentAnimatorStateInfo(0).IsName("Boss_Skill1") && boss.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && boss.isSkill)
            {
                boss.isSkill = false;
                boss.idleState.EnterState();
            }

        }

    }
}
