using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace boss

{
    public class B_Skill_2_State : MonsterState
    {
        Boss_Monster boss;

        
        B_State state = B_State.SKILL_2;
        public B_Skill_2_State(Boss_Monster boss)
        {
            this.boss = boss;
        }
        public void EnterState()
        {
            boss.monsterCurrentStates = this;
            boss.MonsterAnimatorChange((int)state);
            boss.MonsterTriggerChange("Phaze2"); 
            boss.isSkill = true;
            boss.isAttack = false;
            boss.bossCollider.enabled = false;
            boss.nav.ResetPath();
        }

        public void MoveLogic()
        {
            if(boss.isPhaze2Success)
            {
                boss.isSkill = false;
                boss.bossCollider.enabled = true;
                boss.skillCoolTime = 10;
                boss.atkCoolTime = 1.5f;
                boss.idleState.EnterState();
                Debug.Log("페이즈 2 완료");
            }
        }
            
    }
}
