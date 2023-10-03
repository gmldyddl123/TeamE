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
            targetPos = boss.target.position;
            
            boss.bossCollider.enabled = false;
            boss.monsterCurrentStates = this;
            boss.MonsterAnimatorChange((int)state);
            Debug.Log("¹ßµ¿_2");
            boss.isSkillCooldown = true;
            boss.isSkill = true;
            boss.nav.ResetPath();
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
                boss.isSkil_3_On = false;
                boss.isSkill = false;
                boss.bossCollider.enabled = true;
                boss.idleState.EnterState();
            }
          
        }

    }
}
