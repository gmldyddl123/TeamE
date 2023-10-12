using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player
{
    //이제는 사용되지 않습니다 InAirState로 합병
    public class JumpState : PlayerState
    {
        PlayerController playerInputSystem;
        CharacterController characterController;
        //Rigidbody rigidbody;
        //Transform transform;
        //State state = State.Jump;
        private float jumpForce = 3.0f;
        float lastSpeed = 0.0f;

        public JumpState(PlayerController playerInputSystem, CharacterController characterController)
        {
            this.playerInputSystem = playerInputSystem;
            this.characterController = characterController;
            //this.rigidbody = rigidbody;
            //this.transform = transform;
        }

        public void EnterState()
        {
            playerInputSystem.PlayerCurrentStates = this;
            playerInputSystem.moveDirection.y = jumpForce;
            lastSpeed = playerInputSystem.lastMemorySpeed;
        }
        public void MoveLogic()
        {
            Jump();
        }

        public void Jump()
        {

            characterController.Move(
               new Vector3(playerInputSystem.moveDirection.x * lastSpeed,
               playerInputSystem.moveDirection.y * jumpForce,
               playerInputSystem.moveDirection.z * lastSpeed)
               * Time.fixedDeltaTime);

            //characterController.Move(playerInputSystem.moveDirection * 8 * Time.fixedDeltaTime);
            playerInputSystem.InAirUseGravity();
        }
    }
}
