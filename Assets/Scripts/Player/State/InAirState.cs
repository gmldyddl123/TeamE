using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace player
{
    public class InAirState : PlayerState
    {
        PlayerController playerInputSystem;
        CharacterController characterController;
        State state = State.InAir;
        private float jumpForce = 3.0f;
        float lastSpeed = 0.0f;

        public InAirState(PlayerController playerInputSystem, CharacterController characterController)
        {
            this.playerInputSystem = playerInputSystem;
            this.characterController = characterController;
        }

        public void EnterState()
        {
            playerInputSystem.playerCurrentStates = this;
            lastSpeed = playerInputSystem.lastMemorySpeed;
            playerInputSystem.PlayerAnimoatrChage((int)state);
        }
        public void MoveLogic()
        {
            characterController.Move(
              new Vector3(playerInputSystem.moveDirection.x * lastSpeed,
              playerInputSystem.moveDirection.y * jumpForce,
              playerInputSystem.moveDirection.z * lastSpeed)
              * Time.fixedDeltaTime);


            if (playerInputSystem.moveDirection != Vector3.zero)
            {
                playerInputSystem.CheckFrontWall();
                if(playerInputSystem.isWallHit)
                {
                    playerInputSystem.PlayerEnterInAirClimbingState();
                    return;
                }
            }

            playerInputSystem.InAirUseGravity();
        }
    }

}
