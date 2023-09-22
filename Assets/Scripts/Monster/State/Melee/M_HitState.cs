using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace monster

{
    public class M_HitState : MonsterState 
    {
        M_State state = M_State.HIT;
        M_Monster monster;

      
        public M_HitState(M_Monster monster)
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

    
