using monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace L_monster
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

            if (monster.attack_FOV.isCollision && monster.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                monster.long_AttackState.EnterState();
                Debug.Log("변화");
            }

            float distance = Vector3.Distance(monster.target.position, monster.transform.position);
            if (distance > monster.distance)
            {
                monster.chaseState.EnterState();
                Debug.Log("유지");

            }
        }


    }
}

