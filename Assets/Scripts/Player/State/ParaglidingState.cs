using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player
{
    public class ParaglidingState : PlayerState
    {
        PlayerInputSystem playerInputSystem;
        CharacterController characterController;
        //State state = State.Paragliding;
        private float dropSpeed = -3f;
        float paraglidingSpeed = 5.0f;

        public ParaglidingState(PlayerInputSystem playerInputSystem, CharacterController characterController)
        {
            this.playerInputSystem = playerInputSystem;
            this.characterController = characterController;
        }
        public void EnterState()
        {
            playerInputSystem.moveDirection.y = 0;
            playerInputSystem.playerCurrentStates = this;
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


            if(playerInputSystem.moveDirection != Vector3.zero)
            {
                characterController.Move(
                 new Vector3(playerInputSystem.transform.forward.x * paraglidingSpeed,
                 dropSpeed,
                 playerInputSystem.transform.forward.z *paraglidingSpeed)
                 * Time.fixedDeltaTime);
            }
            else
            {
                characterController.Move(new Vector3(0, dropSpeed, 0) * Time.fixedDeltaTime);
            }
            playerInputSystem.PlayerRotateSlerp();
            playerInputSystem.TestLandingGroundCheck();
            //playerInputSystem.UseGravity(dropSpeed);
        }
    }
}

