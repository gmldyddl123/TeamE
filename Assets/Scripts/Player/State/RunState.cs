using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player
{
    public class RunState : PlayerState
    {
        PlayerController playerInputSystem;
        State state = State.RUN;
        private float moveSpeed = 5.0f;
        public RunState(PlayerController playerInputSystem)
        {
            this.playerInputSystem = playerInputSystem;
        }

        public void EnterState()
        {
            playerInputSystem.PlayerCurrentStates = this;
            playerInputSystem.lastMemorySpeed = moveSpeed;
            playerInputSystem.PlayerAnimoatorChage((int)state);
        }

        public void MoveLogic()
        {
            playerInputSystem.MoveToDir();
            playerInputSystem.PlayerMove(moveSpeed);
            //playerInputSystem.PlayerAnimoatrChage((int)state);
        }

    }
}


