using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace monster

{
    public class M_IdleState : MonsterState
    {
        M_Monster monster;
        M_State state = M_State.IDLE;
        public M_IdleState(M_Monster monsterTEST)
        {
            this.monster = monsterTEST;
        }
        public void EnterState()
        {
            monster.monsterCurrentStates = this;
            monster.MonsterAnimatorChange((int)state);
            monster.onMove = true;
            monster.isFriendsAttacked = false;
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