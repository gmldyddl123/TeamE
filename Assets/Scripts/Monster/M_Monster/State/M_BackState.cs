using player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace monster

{
    public class M_BackState : MonsterState
    {
        M_State state = M_State.BACK;
        M_Monster monster;
        
        public M_BackState(M_Monster monster)
        {
            this.monster = monster;
        }

        public void EnterState()
        {
            monster.monsterCurrentStates = this;
            monster.MonsterAnimatorChange((int)state);
            monster.onMove = false;
            monster.isAttack = false;
            monster.attack_FOV.enabled = false;
            monster.nav.ResetPath();
            monster.nav.SetDestination(monster.SpawnPosition);
            Debug.Log(monster.SpawnPosition);
            monster.nav.speed = monster.backSpeed;
             Debug.Log($"µµ∏¡Ω√¿€ : {monster.SpawnPosition}");
            
      }

        public void MoveLogic()
        {
            if (monster.nav.remainingDistance <= 1f)
            {
                monster.isback = false;
                monster.attack_FOV.enabled = true;
                monster.nav.speed = monster.speed;
                monster.idleState.EnterState();
                Debug.Log("µµ∏¡øœ∑·");
                
            }
        }
    }
}

