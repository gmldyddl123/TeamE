using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace player
{
    public class SprintState : PlayerState
    {
        PlayerController playerController;
        State state = State.SPRINT;
        private float moveSpeed = 8.0f;

         

        float staminaMinus = 150.0f;

        public SprintState(PlayerController playerController)
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
            playerController.StaminaConsumption(staminaMinus);
        }

    }
}
