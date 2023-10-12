using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player
{
    public class WalkState : PlayerState
    {
        PlayerController playerController;
        State state = State.WALK;
        private float moveSpeed = 3.0f;
        public WalkState(PlayerController playerController)
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