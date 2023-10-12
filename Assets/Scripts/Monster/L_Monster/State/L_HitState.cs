using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace l_monster

{
    public class L_HitState : MonsterState 
    {
        L_State state = L_State.HIT;
        L_Monster monster;

      
        public L_HitState(L_Monster monster)
        {
            this.monster = monster;
        }

        public void EnterState()
        {
            if(monster.monsterCurrentStates != this)
            {
                monster.monsterCurrentStates = this;
                monster.MonsterAnimatorChange((int)state);
                monster.MonsterHittedChange("Hitted");
            }
            else
            {
                monster.animator.Play("Hit", -1, 0f);
            }
            monster.nav.isStopped = true;
            monster.MonsterAttackChange(false);
        }
        public void MoveLogic()
        {
            
            if (monster.animator.GetCurrentAnimatorStateInfo(0).IsName("Hit") && monster.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                monster.chaseState.EnterState();
            }
            
        }
       
       
    }
}

    
