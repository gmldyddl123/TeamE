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
                Debug.Log("ü�̽�������Ʈ �����");
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
                Debug.Log("2��");
                boss.skill_2_State.EnterState();
                boss.Phaze_2 = true;
            }
            // ��ų���� ���ݸ��� �켱����
            if (boss.FOV1.isCollision 
                && !boss.isAtkCooldown)
            {
                if(a <= 0.5)
                {
                    boss.attack_1_State.EnterState();   //�� �Ʒ� ����
                    Debug.Log("����1");
                }
                else
                {
                    boss.attack_2_State.EnterState();   //�� ����
                    Debug.Log("����2");
                }
            }
            else if (boss.FOV2.isCollision 
                     && !boss.isSkillCooldown)
            {
                if(b <= 0.5)
                {
                    boss.skill_1_State.EnterState();    //��ų 1 �ߵ�
                    Debug.Log("��ų1");
                }
                else
                {
                    boss.skill_3_State.EnterState();    //��ų 2 �ߵ�
                    Debug.Log("��ų2");
                }
            }

            boss.nav.SetDestination(boss.target.position);
        }
            

        

    }
}
