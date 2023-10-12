using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace l_monster

{
    public class L_IdleState : MonsterState
    {
        float time = 0f;
        int a;
        L_Monster monster;
        L_State state = L_State.IDLE;
        public L_IdleState(L_Monster monsterTEST)
        {
            this.monster = monsterTEST;
        }
        public void EnterState()
        {
            monster.monsterCurrentStates = this;
            monster.MonsterAnimatorChange((int)state);
            monster.onMove = true;
            monster.isFriendsAttacked = false;
            a = Random.Range(1, 3);
            WaitTimeCalculate();
        }

        public void MoveLogic()
        {
        
        }

        IEnumerator WaitTimeCalculate()
        {
            while(true) 
            {
                time += Time.deltaTime;

                if(time >= a)
                {
                    monster.walkState.EnterState();
                    yield break;
                }
            }
        }
    }
}