using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BowAimState : PlayerState
{

    State state = State.BowAim;
    PlayerController playerController;
    Animator animator;
    float moveSpeed = 2.0f;

    readonly int X_Hash = Animator.StringToHash("Move_X");
    readonly int Y_Hash = Animator.StringToHash("Move_Y");

    public BowAimState(PlayerController playerController, Animator animator)
    {
        this.playerController = playerController;
        this.animator = animator;
    }

    public void EnterState()
    {
        playerController.playerCurrentStates = this;
        playerController.PlayerAnimoatrChage((int)state);
    }

    public void MoveLogic()
    {
        if(playerController.MoveDir != Vector3.zero)
        {
            playerController.MoveToDir();        
            playerController.PlayerMove(moveSpeed);

        }
       

        animator.SetFloat(X_Hash, playerController.MoveDir.x);
        animator.SetFloat(Y_Hash, playerController.MoveDir.z);
    }

    public void ChangeAnimator(Animator animator)
    {
        this.animator = animator;
    }
}
