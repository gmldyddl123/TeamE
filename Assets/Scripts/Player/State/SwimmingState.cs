using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace player
{
    public class SwimmingState : PlayerState
    {
        PlayerController playerController;
        CharacterController characterController;
        State state = State.Swimming;
        private float moveSpeed = 3.0f;


        Animator animator;
        readonly int SwimmingMove_Hash = Animator.StringToHash("SwimmingMove");
        bool isMove = false;

        public SwimmingState(PlayerController playerController, CharacterController characterController, Animator animator) 
        {
            this.playerController = playerController;
            this.characterController = characterController;
            this.animator = animator;
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

            if(playerController.MoveDir != Vector3.zero)
            {
                characterController.Move(playerController.moveDirection * moveSpeed * Time.fixedDeltaTime);
                isMove = true;
            }
            else
            {
                isMove = false;
            }

            animator.SetBool(SwimmingMove_Hash, isMove);
        }

        public void ExitState()
        {
            playerController.EnterDefalutGroundState();
        }

        public void ChangeAnimator(Animator animator)
        {
            this.animator = animator;
        }
    }
}
