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
            boss.MonsterTriggerChange("Groggy"); 
            boss.boss_CurrentStates = this;
            boss.MonsterAnimatorChange((int)state);
            
            boss.isGroggyCountChange = true;
            Debug.Log("그로기시작");
        }
        
        public void MoveLogic()
        {
            if (boss.isGroggySuccess)
            {
                if(boss.HP <= (boss.MaxHP * 0.5f) && !boss.Phaze_2) 
                {
                    boss.isGroggySuccess = false;
                    boss.Groggy = boss.MaxGroggy;
                    boss.isGroggyCountChange = false;

                    boss.Phaze_2 = true;
                    boss.isPhaze2?.Invoke();
                    Debug.Log("페이즈 2 가야되");
                }
                if(boss.HP > (boss.MaxHP * 0.5f))
                {
                    boss.isGroggySuccess = false;
                    boss.Groggy = boss.MaxGroggy;
                    boss.isGroggyCountChange = false;

                    boss.idleState.EnterState();
                    Debug.Log("아직 페이즈 2갈 피가아님");
                }
                if(boss.HP <= (boss.MaxHP * 0.5f) && boss.Phaze_2)
                {
                    boss.isGroggySuccess = false;
                    boss.Groggy = boss.MaxGroggy;
                    boss.isGroggyCountChange = false;

                    boss.idleState.EnterState();
                    Debug.Log("그로기 완료");
                }
            }
        }

    }
}
