using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlowDownState : PlayerState
{
    PlayerController playerInputSystem;
    //CharacterController characterController;
    //Animator animator;
    State state = State.SlowDown;
    private float lastSpeed;
    private float slowSpeed;

    float timer = 0.0f;

    public SlowDownState(PlayerController playerInputSystem)
    {
        this.playerInputSystem = playerInputSystem;
        //this.animator = animator;
        //this.characterController = characterController;
    }
    public void EnterState()
    {
        playerInputSystem.playerCurrentStates = this;
        lastSpeed = playerInputSystem.lastMemorySpeed;
        playerInputSystem.lastMemorySpeed = 0;
        slowSpeed = lastSpeed * 0.1f;
        
        timer = 0.0f;
        playerInputSystem.PlayerAnimoatorChage((int)state);
    }

    public void MoveLogic()
    {
        timer += Time.deltaTime;

        if (timer <= 0.5f)
            playerInputSystem.PlayerMove(slowSpeed);

        //characterController.Move(playerInputSystem.moveDirection * slowSpeed * Time.fixedDeltaTime);

        //if (timer < animator.GetCurrentAnimatorClipInfo(0)[0].clip.length)
        //{
        //    characterController.Move(playerInputSystem.moveDirection * slowSpeed * Time.fixedDeltaTime);
        //}
        //else
        //{
        //    playerInputSystem.PlayerEnterIdleState();
        //}
    }
}
