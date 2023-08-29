using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace monster

{
    public class M_IdleState : MonsterState
    {
        Monster monster;
        State state = State.IDLE;
        public M_IdleState(Monster monsterTEST)
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