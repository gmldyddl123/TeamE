using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace l_monster

{
    public class L_IdleState : MonsterState
    {
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
            monster.monsterEvents.OnMonsterAttacked += monster.nearbyMonster.ReactToMonsterAttack;
           if(!monster.isStop )
            {
            monster.walkState.EnterState();
            }
            
        }

        public void MoveLogic()
        {
            //ó�� ������ �ѹ��� ���Ǵ� if��
            if (monster.isStop)
            {
                monster.isStop = false;
                monster.walkState.EnterState();
                Debug.Log("�̵�");
            }
        }

    }
}