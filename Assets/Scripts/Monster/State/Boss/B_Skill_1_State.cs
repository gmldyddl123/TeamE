using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace boss

{
    public class B_Skill_1_State : MonsterState
    {
        Boss_Monster boss;
        Vector3 pos = new Vector3 (0, 45, 0);

        Quaternion startRotation; // ���� ȸ��
        Quaternion targetRotation; // ��ǥ ȸ��

        float rotationSpeed = 5;

        B_State state = B_State.SKILL_1;
        public B_Skill_1_State(Boss_Monster boss)
        {
            this.boss = boss;
        }
        public void EnterState()
        {
            boss.monsterCurrentStates = this;
            startRotation = boss.transform.rotation;
            targetRotation = Quaternion.Euler(Vector3.forward + pos);
            boss.transform.LookAt(boss.target.position);
            Debug.Log("�ߵ�_1");
            boss.MonsterAnimatorChange((int)state);
            boss.isSkillCooldown = true;
            boss.nav.ResetPath();
        }

        public void MoveLogic()
        {
           //�ִϸ��̼� Ʋ�����վ �����ʿ�

            if (boss.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                boss.idleState.EnterState();
            }

        }

    }
}
