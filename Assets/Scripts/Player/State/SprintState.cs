using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace player
{
    public class SprintState : PlayerState
    {
        PlayerController playerInputSystem;
        State state = State.SPRINT;
        private float moveSpeed = 8.0f;
        public SprintState(PlayerController playerInputSystem)
        {
            this.playerInputSystem = playerInputSystem;
        }

        public void EnterState()
        {
            playerInputSystem.playerCurrentStates = this;
            playerInputSystem.lastMemorySpeed = moveSpeed;
            playerInputSystem.PlayerAnimoatrChage((int)state);
        }

        public void MoveLogic()
        {
            playerInputSystem.MoveToDir();
            playerInputSystem.PlayerMove(moveSpeed);
        }

    }
}
