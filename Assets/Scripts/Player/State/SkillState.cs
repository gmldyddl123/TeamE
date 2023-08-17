using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace player
{
    public class SkillState : PlayerState
    {
        PlayerController playerController;
        State state = State.Skill;


        public Action onSkillAction;

        public SkillState(PlayerController playerController)
        {
            this.playerController = playerController;
        }

        public void EnterState()
        {
            playerController.playerCurrentStates = this;
            playerController.handWeapon.SetActive(true);
            playerController.backWeapon.SetActive(false);
            playerController.PlayerAnimoatrChage((int)state);
        }

        public void MoveLogic()
        {
            onSkillAction?.Invoke();
        }
    }
}
