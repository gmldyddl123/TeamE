using player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace l_monster
{
    public class L_BackState : MonsterState
    {
        L_State state = L_State.BACK;
        L_Monster monster;

        public L_BackState(L_Monster monster)
        {
            this.monster = monster;
        }

        public void EnterState()
        {
            monster.monsterCurrentStates = this;
            monster.MonsterAnimatorChange((int)state);
            monster.onMove = false;
            monster.isAttack = false;
            monster.nav.ResetPath();
            monster.nav.SetDestination(monster.SpawnPosition);
            monster.nav.speed = monster.backSpeed;
            Debug.Log($"µµ∏¡Ω√¿€ : {monster.SpawnPosition}");

        }

        public void MoveLogic()
        {
            if (monster.nav.remainingDistance <= 1f)
            {
                monster.isback = false;
                monster.nav.speed = monster.speed;
                monster.idleState.EnterState();
                Debug.Log("µµ∏¡øœ∑·");

            }
        }
    }
}

