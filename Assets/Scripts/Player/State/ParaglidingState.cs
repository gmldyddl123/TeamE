using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player
{
    public class ParaglidingState : PlayerState
    {
        PlayerController playerController;
        CharacterController characterController;
        //State state = State.Paragliding;
        private float dropSpeed = -3f;
        float paraglidingSpeed = 5.0f;

        float staminaMinus = 150.0f;

        public ParaglidingState(PlayerController playerInputSystem, CharacterController playerController)
        {
            this.playerController = playerInputSystem;
            this.characterController = playerController;
        }
        public void EnterState()
        {
            playerController.moveDirection.y = 0;
            playerController.PlayerCurrentStates = this;
            //playerInputSystem.PlayerAnimoatrChage((int)state);
        }

        public void MoveLogic()
        {
            //playerInputSystem.MoveToDir();
            //playerInputSystem.PlayerMove(paraglidingSpeed);

            //characterController.Move(
            // new Vector3(playerInputSystem.moveDirection.x * paraglidingSpeed,
            // playerInputSystem.moveDirection.y,
            // playerInputSystem.moveDirection.z * paraglidingSpeed)
            // * Time.fixedDeltaTime);

            //characterController.Move(
            // new Vector3(playerInputSystem.moveDirection.x * Mathf.Abs(playerInputSystem.transform.forward.x) * paraglidingSpeed,
            // playerInputSystem.moveDirection.y,
            // playerInputSystem.moveDirection.z * Mathf.Abs(playerInputSystem.transform.forward.z) * paraglidingSpeed)
            // * Time.fixedDeltaTime);


            //characterController.Move(characterController.transform.forward * paraglidingSpeed * Time.fixedDeltaTime);


            if(playerController.moveDirection != Vector3.zero)
            {
                characterController.Move(
                 new Vector3(playerController.transform.forward.x * paraglidingSpeed,
                 dropSpeed,
                 playerController.transform.forward.z *paraglidingSpeed)
                 * Time.fixedDeltaTime);
            }
            else
            {
                characterController.Move(new Vector3(0, dropSpeed, 0) * Time.fixedDeltaTime);
            }
            playerController.PlayerRotateSlerp();
            playerController.TestLandingGroundCheck();
            playerController.StaminaConsumption(staminaMinus);
            //playerInputSystem.UseGravity(dropSpeed);
        }
    }
}

