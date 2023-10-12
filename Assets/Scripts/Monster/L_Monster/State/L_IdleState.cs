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
        bool isWait = true;
        L_Monster monster;
        L_State state = L_State.IDLE;
        public L_IdleState(L_Monster monsterTEST)
        {
            this.monster = monsterTEST;
        }
        public void EnterState()
        {
            monster.nav.ResetPath();
            monster.MonsterAnimatorChange((int)state);
            a = Random.Range(1, 3);
            isWait = true;
            monster.monsterCurrentStates = this;
            monster.onMove = true;
            monster.isFriendsAttacked = false;
        }

        public void MoveLogic()
        {
            if (isWait)
            { 
                time += Time.deltaTime;

                if (time >= a)
                {
                    isWait = false;
                    monster.walkState.EnterState();
                    time = 0;
                }
            }
        }
  
    }
}