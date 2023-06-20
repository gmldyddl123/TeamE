using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player
{
    //������ ������ �ʽ��ϴ� InAirState�� �պ�
    public class JumpState : PlayerState
    {
        PlayerInputSystem playerInputSystem;
        CharacterController characterController;
        //Rigidbody rigidbody;
        //Transform transform;
        //State state = State.Jump;
        private float jumpForce = 3.0f;
        float lastSpeed = 0.0f;

        public JumpState(PlayerInputSystem playerInputSystem, CharacterController characterController)
        {
            this.playerInputSystem = playerInputSystem;
            this.characterController = characterController;
            //this.rigidbody = rigidbody;
            //this.transform = transform;
        }

        public void EnterState()
        {
            playerInputSystem.playerCurrentStates = this;
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
            playerInputSystem.UseGravity();
        }
    }
}
