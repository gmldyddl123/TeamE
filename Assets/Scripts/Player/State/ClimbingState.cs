using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClimbingState : PlayerState
{
    PlayerController playerController;
    CharacterController characterController;
    State state = State.Climbing;


    private float wallMoveSpeed = 2.0f;

    Vector3 inputMoveDirection;

    RaycastHit hitinfo;
    //Vector3 moveMoveDirection;

    //bool isLeftHand = false;
    //bool isMove = false;

    public ClimbingState(PlayerController playerController, CharacterController characterController)
    {
        this.playerController = playerController;
        this.characterController = characterController;
    }

    public void EnterState()
    {
        playerController.playerCurrentStates = this;
        playerController.PlayerAnimoatrChage((int)state);
    }

    public void MoveLogic() //이동은상시되고 점프와 끝까지 올랐을때 행동이 막혀야함
    {
        //inputMoveDirection = Vector3.right * playerController.moveDirection.x;
        //inputMoveDirection += Vector3.up * playerController.moveDirection.z;
        //inputMoveDirection = new Vector3(playerController.MoveDir.x, playerController.MoveDir.z, playerController.MoveDir.x);
        
        

        if(playerController.MoveDir != Vector3.zero)
        {
            inputMoveDirection = Vector3.right * playerController.MoveDir.x;
            inputMoveDirection += Vector3.up * playerController.MoveDir.z;
            inputMoveDirection = playerController.transform.TransformDirection(inputMoveDirection);
            Debug.Log(inputMoveDirection);

            if (Physics.Raycast(playerController.transform.position, playerController.transform.forward, out hitinfo, 1.5f))
            {
                playerController.transform.rotation = Quaternion.LookRotation(-hitinfo.normal);
            }

            characterController.Move(inputMoveDirection * wallMoveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            inputMoveDirection = Vector3.zero;
        }



        //if (!isMove && inputMoveDirection != Vector3.zero)
        //{
        //    isMove = true;
        //    isLeftHand = !isLeftHand;
        //    moveMoveDirection = inputMoveDirection;
        //}
        //else
        //{

        //}
    }

}
