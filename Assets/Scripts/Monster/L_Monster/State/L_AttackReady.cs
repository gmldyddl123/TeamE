using monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace l_monster
{
    public class L_AttackReady : MonsterState
    {
        L_Monster monster;
        L_State state = L_State.ATTACKREADY;

        public L_AttackReady(L_Monster monster)
        {

            this.monster = monster;
        }
        public void EnterState()
        {
            monster.monsterCurrentStates = this;
            monster.MonsterAnimatorChange((int)state);
            monster.nav.ResetPath();
        }

        public void MoveLogic()
        {
            Vector3 direction = monster.target.position - monster.transform.position;
            direction.y = 0;
            monster.targetRotation = Quaternion.LookRotation(direction);
            monster.targetRotation *= Quaternion.Euler(0, 90, 0);
            monster.transform.rotation = Quaternion.Slerp(monster.transform.rotation, monster.targetRotation, monster.rotationSpeed * Time.deltaTime);
         
            if (!monster.FOV1.isCollision)
            {
                monster.chaseState.EnterState();
                
                Debug.Log("À¯Áö");

            }
        }


    }
}

