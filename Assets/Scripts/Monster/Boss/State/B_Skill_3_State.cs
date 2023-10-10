using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

namespace boss

{
    public class B_Skill_3_State : MonsterState
    {
        Boss_Monster boss;
        B_State state = B_State.SKILL_3;

        Vector3 targetPos;
        Vector3 pos;
        public B_Skill_3_State(Boss_Monster boss)
        {
            this.boss = boss;
        }
        public void EnterState()
        {
            boss.MonsterAnimatorChange((int)state);
            targetPos = boss.target.position;
            //boss.isSkillCooldown = true;
            
            boss.isSkill = true;
            boss.nav.ResetPath();
            boss.bossCollider.enabled = false;
            boss.boss_CurrentStates = this;
            Debug.Log("¹ßµ¿_2");
            Debug.Log(boss.isSkillCooldown);
        }

        public void MoveLogic()
        {
            Vector3 direction = targetPos - boss.transform.position;
            direction.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            Quaternion offsetRotation = Quaternion.Euler(0, 0, 0);

            targetRotation *= offsetRotation;

            boss.transform.rotation = Quaternion.Slerp(boss.transform.rotation, targetRotation, Time.deltaTime * boss.rotationSpeed);

           if(boss.animator.GetCurrentAnimatorStateInfo(0).IsName("Boss_Skill3")
                && boss.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f
                && boss.isSkill)
            {
                boss.isSkill = false;
                boss.coolReset = false;
                boss.bossCollider.enabled = true;
                boss.idleState.EnterState();
            }
          
        }

    }
}
