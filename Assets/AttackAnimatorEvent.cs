using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimatorEvent : StateMachineBehaviour
{
    //OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    animator.GetComponent<PlayerStat>().TestResetAttackMove();

    //    //controller.currentPlayerCharater.TestResetAttackMove();
    //}

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<PlayerStat>().TestResetAttackMove();
    }

}
