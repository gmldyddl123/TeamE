using player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace boss

{
    public class B_GroggyState : MonsterState
    {
        Boss_Monster boss;
        B_State state = B_State.GROGGY;
        public B_GroggyState(Boss_Monster boss)
        {
            this.boss = boss;
        }
        public void EnterState()
        {
            boss.monsterCurrentStates = this;
            boss.MonsterAnimatorChange((int)state);
            boss.MonsterTriggerChange("Groggy"); 
            Debug.Log("�׷α�");
        }
        
        public void MoveLogic()
        {
            Debug.Log("�׷α� ������");
            if (boss.isGroggySuccess)
            {
                if(boss.HP <= boss.MaxHP * 0.5f) 
                {
                    boss.Groggy = boss.MaxGroggy;
                    boss.Phaze_2 = true;
                    boss.isPhaze2?.Invoke();
                    Debug.Log("������ 2 ���ߵ�");
                }
                else
                {
                    boss.Groggy = boss.MaxGroggy;
                    boss.idleState.EnterState();
                    Debug.Log("���� ������ 2�� �ǰ��ƴ�");
                }
            }
        }

    }
}
