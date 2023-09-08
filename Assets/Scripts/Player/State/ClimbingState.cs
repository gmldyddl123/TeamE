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

    Quaternion targetRotation;
    Quaternion playerRoation;

    private float wallMoveSpeed = 1.0f;
    private float pushPower = 1f;
    private float rotationSpeed = 5f;


    Vector3 inputMoveDirection;

    Vector3 lastMemoryClimbingMoveRotateHitVector;
    bool turnHitTiming = true;


    Transform controllerTransform;
    Transform wallDirRayStartPos;


    Transform rightToLeftRay;
    Transform leftToRightRay;

    int playerLayerMask = (-1) - (1 << LayerMask.NameToLayer("Player"));

    RaycastHit hitinfo;
    //Vector3 moveMoveDirection;

    //bool isLeftHand = false;
    //bool isMove = false;


    bool firstUpSet = true;
    float timer = 0;


    Animator animator;
    readonly int X_Hash = Animator.StringToHash("Climbing_X");
    readonly int Y_Hash = Animator.StringToHash("Climbing_Y");

    bool isLeftHandUp = true;
    readonly int HandChange_Hash = Animator.StringToHash("IsLeftHandUp");

    public ClimbingState(PlayerController playerController, CharacterController characterController, Animator animator)
    {
        this.playerController = playerController;
        this.characterController = characterController;
        this.animator = animator;
        controllerTransform = characterController.transform;
        wallDirRayStartPos = playerController.wallDirCheckPos;

        rightToLeftRay = playerController.rightToLeftRay;
        leftToRightRay = playerController.leftToRightRay;


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
                //targetRotation = Quaternion.LookRotation(-hitinfo.normal);
                //playerRoation = Quaternion.Slerp(controllerTransform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            }

            characterController.Move(controllerTransform.TransformDirection(Vector3.up) * 1.0f * Time.fixedDeltaTime);

            timer += Time.fixedDeltaTime;
            if(timer > 0.5f)
            {
                firstUpSet = false;
                //controllerTransform.rotation = Quaternion.LookRotation(-hitinfo.normal);

                targetRotation = Quaternion.LookRotation(-hitinfo.normal);
                playerRoation = Quaternion.Slerp(controllerTransform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
                //controllerTransform.rotation = Quaternion.LookRotation(-playerController.ClimbingMoveRotateHitVector);

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

    
            if(playerController.ClimbingMoveRotateHitVector != Vector3.zero)
            {

                if(lastMemoryClimbingMoveRotateHitVector != playerController.ClimbingMoveRotateHitVector)
                {
                    turnHitTiming = true;

                }

                if(turnHitTiming)
                {
                    //controllerTransform.rotation = Quaternion.LookRotation(-playerController.ClimbingMoveRotateHitVector);

                    //controllerTransform.rotation = Quaternion.LookRotation(-playerController.ClimbingMoveRotateHitVector);
                    lastMemoryClimbingMoveRotateHitVector = playerController.ClimbingMoveRotateHitVector;
                    targetRotation = Quaternion.LookRotation(-playerController.ClimbingMoveRotateHitVector);

                    playerRoation = Quaternion.Slerp(controllerTransform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
                    controllerTransform.rotation = playerRoation;
                    //controllerTransform.rotation = Quaternion.Lerp(controllerTransform.rotation, Quaternion.LookRotation(-playerController.ClimbingMoveRotateHitVector), Time.deltaTime * rotationSpeed);
                }
              
            }
            //바로 아래 지운거 레이캐스트로 기울기 정해주는거



            //if (Physics.Raycast(wallDirRayStartPos.position, wallDirRayStartPos.forward, out hitinfo, 1f))
            //{
            //    Debug.Log("인");
            //    targetRotation = Quaternion.LookRotation(-hitinfo.normal);
            //    //Quaternion.Slerp(controllerTransform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            //    //playerRoation = Quaternion.Slerp(controllerTransform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            //    controllerTransform.rotation = Quaternion.Slerp(controllerTransform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            //    //controllerTransform.rotation = Quaternion.LookRotation(-hitinfo.normal);
            //}

            //else
            //{
            //    //characterController.Move(controllerTransform.TransformDirection(-playerController.climbingMoveRotateHitVector) * pushPower * Time.fixedDeltaTime);
            //    characterController.Move(controllerTransform.TransformDirection(Vector3.forward) * pushPower * Time.fixedDeltaTime);
            //}


            inputMoveDirection = Vector3.right * playerController.MoveDir.x;
            inputMoveDirection += Vector3.up * playerController.MoveDir.z;
            //inputMoveDirection += Vector3.forward * pushPower;
            inputMoveDirection = controllerTransform.TransformDirection(inputMoveDirection);



            if (playerController.MoveDir.x != 0 && !Physics.Raycast(wallDirRayStartPos.position + wallDirRayStartPos.TransformDirection(new Vector3(0.3f * playerController.MoveDir.x, 0, -1.0f)), wallDirRayStartPos.forward, out hitinfo, 1.5f))
            {
                turnHitTiming = false;

            }
            if(!turnHitTiming)
            {
                if (playerController.MoveDir.x > 0)
                {
                    if (Physics.Raycast(rightToLeftRay.position, rightToLeftRay.forward, out hitinfo, 1.0f, playerLayerMask))
                    {
                        //controllerTransform.rotation = Quaternion.LookRotation(-hitinfo.normal);
                        targetRotation = Quaternion.LookRotation(-hitinfo.normal);
                        playerRoation = Quaternion.Slerp(controllerTransform.rotation, targetRotation, rotationSpeed * 2 * Time.fixedDeltaTime);
                        controllerTransform.rotation = playerRoation;
                    }
                }
                else if (playerController.MoveDir.x < 0)
                {
                    if (Physics.Raycast(leftToRightRay.position, leftToRightRay.forward, out hitinfo, 1.0f, playerLayerMask))
                    {
                        targetRotation = Quaternion.LookRotation(-hitinfo.normal);
                        playerRoation = Quaternion.Slerp(controllerTransform.rotation, targetRotation, rotationSpeed * 2 * Time.fixedDeltaTime);
                        controllerTransform.rotation = playerRoation;
                    }

                }
            }

            //압착(중력)
            if (!Physics.Raycast(wallDirRayStartPos.position , wallDirRayStartPos.forward, 0.25f, playerLayerMask))
            {
                characterController.Move(controllerTransform.TransformDirection(Vector3.forward) * pushPower * Time.fixedDeltaTime);
            }
            //if (Physics.Raycast(wallDirRayStartPos.position, wallDirRayStartPos.forward, out hitinfo, 1f))
            //{
            //    Debug.Log("인");
            //    targetRotation = Quaternion.LookRotation(-hitinfo.normal);
            //    //Quaternion.Slerp(controllerTransform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            //    playerRoation = Quaternion.Slerp(controllerTransform.rotation, targetRotation, rotationSpeed * 2 * Time.fixedDeltaTime);
            //    controllerTransform.rotation = playerRoation;
            //    //controllerTransform.rotation = Quaternion.LookRotation(-hitinfo.normal);
            //}


            characterController.Move(inputMoveDirection * wallMoveSpeed * Time.fixedDeltaTime);

         

         

            if (playerController.MoveDir.z != 0 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                isLeftHandUp = !isLeftHandUp;
                animator.SetBool(HandChange_Hash, isLeftHandUp);
            }
            controllerTransform.rotation = playerRoation;

        }
        else
        {
            inputMoveDirection = Vector3.zero;
        }

        Debug.Log(turnHitTiming);
     


        //if (Physics.Raycast(wallDirRayStartPos.position, wallDirRayStartPos.forward, out hitinfo, 2f))
        //{
        //    targetRotation = Quaternion.LookRotation(-hitinfo.normal);
        //    //Quaternion.Slerp(controllerTransform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        //    playerRoation = Quaternion.Slerp(controllerTransform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);


        //    characterController.Move(controllerTransform.TransformDirection(Vector3.forward) * 3 * Time.fixedDeltaTime);
        //}

        //Quaternion.Slerp(controllerTransform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        //controllerTransform.rotation = playerRoation;
        animator.SetFloat(X_Hash, playerController.MoveDir.x);
        animator.SetFloat(Y_Hash, playerController.MoveDir.z);

    }

 

}
