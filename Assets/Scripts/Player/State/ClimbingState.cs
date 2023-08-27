using player;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ClimbingState : PlayerState
{
    PlayerController playerController;
    CharacterController characterController;
    State state = State.Climbing;


    private float wallMoveSpeed = 2.0f;
    private float pushPower = 3.0f;


    Vector3 inputMoveDirection;


    Transform controllerTransform;
    Transform wallDirRayStartPos;

    RaycastHit hitinfo;
    //Vector3 moveMoveDirection;

    //bool isLeftHand = false;
    //bool isMove = false;


    bool firstUpSet = true;
    float timer = 0;

    public ClimbingState(PlayerController playerController, CharacterController characterController)
    {
        this.playerController = playerController;
        this.characterController = characterController;
        controllerTransform = characterController.transform;
        wallDirRayStartPos = playerController.wallDirCheckPos;

        firstUpSet = true;
        timer = 0;
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

        if(firstUpSet)
        {
            if (Physics.Raycast(wallDirRayStartPos.position, wallDirRayStartPos.forward, out hitinfo, 0.4f))
            {
                controllerTransform.rotation = Quaternion.LookRotation(-hitinfo.normal);
            }

            characterController.Move(controllerTransform.TransformDirection(Vector3.up) * 1.0f * Time.fixedDeltaTime);

            timer += Time.fixedDeltaTime;
            if(timer > 0.5f)
            {
                firstUpSet = false;
                //controllerTransform.rotation = Quaternion.LookRotation(-hitinfo.normal);
                controllerTransform.rotation = Quaternion.LookRotation(-playerController.ClimbingMoveRotateHitVector);

            }
            return;
        }


        if (playerController.MoveDir != Vector3.zero)
        {
           

            //if (playerController.MoveDir.x != 0)
            //{
            //    //법선 회전
            //    controllerTransform.rotation = Quaternion.LookRotation(-playerController.climbingMoveRotateHitVector);

            //    //if (Physics.Raycast(wallDirRayStartPos.position, controllerTransform.right * playerController.MoveDir.x, out hitinfo, 1f))
            //    //{
            //    //    controllerTransform.rotation = Quaternion.LookRotation(-playerController.climbingMoveRotateHitVector);
            //    //}

            //}

            controllerTransform.rotation = Quaternion.LookRotation(-playerController.ClimbingMoveRotateHitVector);
            Debug.Log(-playerController.ClimbingMoveRotateHitVector);

            if (Physics.Raycast(wallDirRayStartPos.position, wallDirRayStartPos.forward, out hitinfo,0.4f))
            {
                controllerTransform.rotation = Quaternion.LookRotation(-hitinfo.normal);
            }

            else
            {
                //characterController.Move(controllerTransform.TransformDirection(-playerController.climbingMoveRotateHitVector) * pushPower * Time.fixedDeltaTime);
                characterController.Move(controllerTransform.TransformDirection(Vector3.forward) * pushPower * Time.fixedDeltaTime);
            }

            inputMoveDirection = Vector3.right * playerController.MoveDir.x;
            inputMoveDirection += Vector3.up * playerController.MoveDir.z;
            //inputMoveDirection += Vector3.forward * pushPower;
            inputMoveDirection = controllerTransform.TransformDirection(inputMoveDirection);



            //float rot = Vector3.Dot(hitinfo.point, playerController.climbingMoveRotateHitVector);
            //Debug.Log(rot);

            //if (Physics.Raycast(wallDirRayStartPos.position, controllerTransform.forward, out hitinfo, 0.4f))
            //{
            //    //레이 회전
            //    playerController.transform.rotation = Quaternion.LookRotation(-hitinfo.normal);

            //    //float rot = Vector3.Dot(hitinfo.point, playerController.climbingMoveRotateHitVector);
            //    //Debug.Log(rot);
            //    //playerController.transform.rotation = Quaternion.LookRotation(-hitinfo.normal);
            //    //playerController.transform.rotation = Quaternion.LookRotation(rot);
            //    //Quaternion.AngleAxis(rot, Vector3.up);

            //    //playerController.transform.rotation = Quaternion.LookRotation(-playerController.climbingMoveRotateHitVector);
            //    //playerController.transform.eulerAngles = Vector3.up * rot;

            //}
            //else
            //{
            //    characterController.Move(controllerTransform.TransformDirection(Vector3.forward) * pushPower * Time.fixedDeltaTime);
            //}



            //법선 회전
            //playerController.transform.rotation = Quaternion.LookRotation(-playerController.climbingMoveRotateHitVector);


            //Debug.Log(rot);
            //Vector3 test = Vector3.Cross(playerController.transform.forward, playerController.climbingMoveRotateHitVector);






            //playerController.transform.rotation = Quaternion.LookRotation(test);
            //playerController.transform.Rotate(playerController.transform.position, rot, Space.World) ;//내적 테스트

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
