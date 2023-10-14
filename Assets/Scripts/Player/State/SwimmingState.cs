using player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

        float staminaMinus = 30.0f;

        float waterHeight;
        float minHeight;

        float upForce = 3.0f;
        float downForce = 2.0f;

        bool divingDive = false;

        bool dive = false;
        
        bool boolDiveLogicComplete = false;


        public SwimmingState(PlayerController playerController, CharacterController characterController, Animator animator) 
        {
            this.playerController = playerController;
            this.characterController = characterController;
            this.animator = animator;
        }

        public void EnterState()
        {
            playerController.PlayerAnimoatorChage((int)state);
            playerController.MoveToDir();

            waterHeight = playerController.transform.position.y;
            boolDiveLogicComplete = false;
            if (playerController.IsInAir)
            {
                divingDive = true;
                dive = true;


                minHeight = waterHeight - 2.0f;

                playerController.IsInAir = false;

                if(playerController.MoveDir == Vector3.zero)
                {
                    playerController.moveDirection = Vector3.zero;
                }

                playerController.moveDirection.y = -5.1f;
            }
            else
            {
                divingDive = false;
                dive = false;
                boolDiveLogicComplete = false;
                playerController.moveDirection.y = 0;
            }

            if (playerController.MoveDir != Vector3.zero)
            {
                isMove = true;
                playerController.lastMemorySpeed = moveSpeed;
                animator.SetBool(SwimmingMove_Hash, isMove);
            }
            else
            {
                playerController.lastMemorySpeed = 0;
                isMove = false;
            }
            playerController.WaterDive = divingDive;
            playerController.PlayerCurrentStates = this;

        }

        public void MoveLogic()
        {
            playerController.MoveToDir();




            if (divingDive)
            {
                //playerController.moveDirection.y = -3f;
                playerController.moveDirection.y += 6.81f * Time.fixedDeltaTime;
                characterController.Move(
                    new Vector3(playerController.moveDirection.x * moveSpeed,
              playerController.moveDirection.y,
              playerController.moveDirection.z * moveSpeed)
              * Time.fixedDeltaTime);

                //  playerController.moveDirection.y += 3.81f * Time.fixedDeltaTime;
                //  characterController.Move(
                //      new Vector3(playerController.moveDirection.x * moveSpeed,
                //0,
                //playerController.moveDirection.z * moveSpeed)
                //* Time.fixedDeltaTime);


                //  characterController.Move(
                //      new Vector3(0,
                //playerController.moveDirection.y * Time.fixedDeltaTime,
                //0));

                //  playerController.moveDirection.y += 6.81f * Time.fixedDeltaTime;


                if (playerController.MoveDir != Vector3.zero)
                {
                    isMove = true;
                }
                else
                {
                    isMove = false;
                }

                if (playerController.transform.position.y > waterHeight)
                {
                    divingDive = false;
                    playerController.WaterDive = divingDive;
                    playerController.moveDirection.y = 0f;
                    //playerController.IsInAir = false;
                }
            }

            else
            {
                playerController.moveDirection.y = 0f;
                if (playerController.MoveDir != Vector3.zero)
                {
                    //playerController.moveDirection.y = 0;
                    playerController.StaminaConsumption(staminaMinus);
                    characterController.Move(playerController.moveDirection * moveSpeed * Time.fixedDeltaTime);
                    isMove = true;
                    playerController.lastMemorySpeed = moveSpeed;
                }
                else
                {
                    playerController.lastMemorySpeed = 0f;
                    isMove = false;
                }
            }





            animator.SetBool(SwimmingMove_Hash, isMove);



            //if (dive && !boolDiveLogicComplete)
            //{
            //    if (playerController.transform.position.y > minHeight)
            //    {
            //        playerController.moveDirection.y -= 3.81f * Time.fixedDeltaTime;
            //        characterController.Move(playerController.moveDirection * moveSpeed * Time.fixedDeltaTime);
            //    }
            //    else
            //    {
            //        dive = false;
            //    }
            //    return;
            //}
            //else
            //{
            //    if (playerController.transform.position.y < waterHeight)
            //    {
            //        playerController.moveDirection.y += 3.81f * Time.fixedDeltaTime;
            //        characterController.Move(playerController.moveDirection * moveSpeed * Time.fixedDeltaTime);
            //    }
            //    else
            //    {
            //        boolDiveLogicComplete = true;
            //    }
            //    return;
            //}


            //playerController.moveDirection.y = 0f;
            //if (playerController.MoveDir != Vector3.zero)
            //{
            //    //playerController.moveDirection.y = 0;
            //    playerController.StaminaConsumption(staminaMinus);
            //    characterController.Move(playerController.moveDirection * moveSpeed * Time.fixedDeltaTime);
            //    isMove = true;
            //    playerController.lastMemorySpeed = moveSpeed;
            //}
            //else
            //{
            //    playerController.lastMemorySpeed = 0f;
            //    isMove = false;
            //}

        }

        public void ExitState()
        {
            if(!playerController.IsInAir && !divingDive)
            {
                playerController.EnterDefalutGroundState();
            }
        }

        public void ChangeAnimator(Animator animator)
        {
            this.animator = animator;
        }
    }
}
