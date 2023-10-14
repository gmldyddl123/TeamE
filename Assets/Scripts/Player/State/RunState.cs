using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player
{
    public class RunState : PlayerState
    {
        PlayerController playerController;
        State state = State.RUN;
        private float moveSpeed = 5.0f;
        public RunState(PlayerController playerController)
        {
            this.playerController = playerController;
        }

        public void EnterState()
        {
            playerController.PlayerCurrentStates = this;
            playerController.lastMemorySpeed = moveSpeed;
            playerController.PlayerAnimoatorChage((int)state);
        }

        public void MoveLogic()
        {
            playerController.MoveToDir();
            playerController.PlayerMove(moveSpeed);
            //playerInputSystem.PlayerAnimoatrChage((int)state);
        }

    }
}


