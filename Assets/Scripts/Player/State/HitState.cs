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



        public HitState(PlayerController playerController, CharacterController characterController)
        {
            this.playerController = playerController;
            this.characterController = characterController;
        }

        public void EnterState()
        {
            playerController.playerCurrentStates = this;
            playerController.PlayerAnimoatrChage((int)state);
        }

        public void MoveLogic()
        {

        }
    }
}


