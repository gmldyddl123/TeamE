using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player
{
    public class HitState : PlayerState
    {
        PlayerController playerController;
        CharacterController characterController;
        State state = State.Hit;

        bool knockback = false;

        public HitState(PlayerController playerController, CharacterController characterController)
        {
            this.playerController = playerController;
            this.characterController = characterController;
            
        }

        public void EnterState()
        {
            knockback = playerController.Knockback;

            playerController.playerCurrentStates = this;
            playerController.PlayerAnimoatorChage((int)state);
            Debug.Log("Hit");
        }

        public void MoveLogic()
        {
            if(knockback)
            {

            }
        }

    }
}


