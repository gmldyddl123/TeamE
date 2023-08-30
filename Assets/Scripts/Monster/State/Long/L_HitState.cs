using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace monster

{
    public class L_HitState : MonsterState 
    {
        State state = State.L_HIT;
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
            }
            else
            {
                monster.animator.Play("Hit", -1, 0f);
            }
            monster.nav.isStopped = true;
            monster.MonsterAnimationChange(false);
            monster.attack_FOV.gameObject.SetActive(false);
        }
        public void MoveLogic()
        {
            
            if (monster.animator.GetCurrentAnimatorStateInfo(0).IsName("Hit") && monster.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                monster.attack_FOV.gameObject.SetActive(true);
                monster.chaseState.EnterState();
            }
            
        }
       
       
    }
}

    
