using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingState : PlayerState
{
    PlayerController playerInputSystem;
    State state = State.Climbing;
    private float wallMoveSpeed = 3.0f;
    public ClimbingState(PlayerController playerInputSystem)
    {
        this.playerInputSystem = playerInputSystem;
    }

    public void EnterState()
    {
        playerInputSystem.playerCurrentStates = this;
        playerInputSystem.PlayerAnimoatrChage((int)state);
    }

    public void MoveLogic()
    {
        
    }
}
