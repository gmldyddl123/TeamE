using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace player
{
    public class HitState : PlayerState
    {
        PlayerController playerController;
        CharacterController characterController;
        State state = State.Hit;

        bool knockback = false;

        Vector3 knockBackDir = Vector3.zero;

        float knockBack_Y_Power = 3.0f;
        float knockBackSpeed = 3.0f;


        public HitState(PlayerController playerController, CharacterController characterController)
        {
            this.playerController = playerController;
            this.characterController = characterController;
            
        }

        public void EnterState()
        {

            if(playerController.PlayerCurrentStates is AttackState)
            {
                AttackState attack = playerController.PlayerCurrentStates as AttackState;
                attack.ExitAttackState();
            }

            knockback = playerController.Knockback;
            if(knockback)
            {
                playerController.moveDirection = Vector3.zero;

                Vector3 dir = playerController.transform.position - playerController.AttackHitPos;
                dir.y = 1;

                //≥ÀπÈ πÊ«‚ ∏∏µÈæÓ¡÷±‚
                //knockBackDir = new
                //(
                //    -playerController.AttackHitPos.x,
                //    knockBack_Y_Power,
                //    -playerController.AttackHitPos.z
                //);


                //playerController.moveDirection = knockBackDir;
                playerController.moveDirection = dir.normalized;

            }


            playerController.PlayerCurrentStates = this;
            playerController.PlayerAnimoatorChage((int)state);
            Debug.Log("Hit");
        }


        public void MoveLogic()
        {
            if(knockback)
            {
                characterController.Move(
               new Vector3(playerController.moveDirection.x * knockBackSpeed,
               playerController.moveDirection.y * knockBack_Y_Power,
               playerController.moveDirection.z * knockBackSpeed) * Time.fixedDeltaTime);


                UseGravity(-2.0f);
            } 
        }

        public void UseGravity(float gravity = -9.81f) //∫Ò«‡¡ﬂ ≥´«œ
        {
            if (characterController.isGrounded == false)
            {
                if (playerController.moveDirection.y > -10f)
                    playerController.moveDirection.y += gravity * Time.fixedDeltaTime;
            }
            else
            {
                //∂•ø° ¥Í¿Ω
                knockback = false;
                playerController.Knockback = knockback;
            }
        }

    }
}


