using player;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.PackageManager;
using UnityEngine;

public class ClimbingState : PlayerState
{
    PlayerController playerController;
    CharacterController characterController;
    State state = State.Climbing;

    Quaternion targetRotation;
    Quaternion playerRoation;

    private float wallMoveSpeed = 1.0f;
    //private float wallMoveSpeed = 1.0f;


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


    bool inAirEnter = false;
    bool firstUpSet = true;
    bool endUpSet = false;
    bool endDownSet = false;

    float firstUpSpeed = 1.0f;
    float lastUpSpeed = 3.0f;
    float lastPushPower = 2.0f;


    float timer = 0;


    Animator animator;
    readonly int X_Hash = Animator.StringToHash("Move_X");
    readonly int Y_Hash = Animator.StringToHash("Move_Y");

    bool isLeftHandUp = true;
    readonly int HandChange_Hash = Animator.StringToHash("IsLeftHandUp");


    //스태미나 소모
    float staminaMinus = 5.0f;

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
        firstUpSet = true;
        endUpSet = false;
        endDownSet = false;
        isLeftHandUp = true;
        turnHitTiming = false;
        playerController.ClimbingMoveRotateHitVector = Vector3.zero;
        playerController.exitWallState = false;
        playerController.completeWallRaiseUp = false;
        timer = 0;
        Debug.Log(hitinfo.normal) ;

        if(playerController.PlayerCurrentStates is InAirState)
        {
            inAirEnter = true;
            isLeftHandUp = false;
        }

        animator.SetBool(HandChange_Hash, isLeftHandUp);
        playerController.PlayerCurrentStates = this;
        playerController.PlayerAnimoatorChage((int)state);
    }

    void ExitState()
    {
        firstUpSet = true;
        endUpSet = false;
        endDownSet = false;
        isLeftHandUp = true;
        animator.SetBool(HandChange_Hash, isLeftHandUp);
        playerController.exitWallState = false;
        playerController.completeWallRaiseUp = false;
        timer = 0;
        playerController.completeWallRaiseUp = false;
    }

    public void MoveLogic() //이동은상시되고 점프와 끝까지 올랐을때 행동이 막혀야함
    {
        //inputMoveDirection = Vector3.right * playerController.moveDirection.x;
        //inputMoveDirection += Vector3.up * playerController.moveDirection.z;
        //inputMoveDirection = new Vector3(playerController.MoveDir.x, playerController.MoveDir.z, playerController.MoveDir.x);

        //시작할때 살작 올라가기
        if(firstUpSet)
        {
            if(inAirEnter)
            {
                if (Physics.Raycast(wallDirRayStartPos.position, wallDirRayStartPos.forward, out hitinfo, 0.4f))
                {
                    controllerTransform.rotation = Quaternion.LookRotation(-hitinfo.normal);
                }

                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
                {
                    firstUpSet = false;
                    targetRotation = Quaternion.LookRotation(-hitinfo.normal);
                    playerRoation = Quaternion.Slerp(controllerTransform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
                }
            }
            else
            {
                if (Physics.Raycast(wallDirRayStartPos.position, wallDirRayStartPos.forward, out hitinfo, 0.4f))
                {
                    controllerTransform.rotation = Quaternion.LookRotation(-hitinfo.normal);

                }

                characterController.Move(controllerTransform.TransformDirection(Vector3.up) * firstUpSpeed * Time.fixedDeltaTime);

                timer += Time.fixedDeltaTime;
                if (timer > 0.5f)
                {
                    firstUpSet = false;
                    timer = 0.0f;
                    targetRotation = Quaternion.LookRotation(-hitinfo.normal);
                    playerRoation = Quaternion.Slerp(controllerTransform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);


                }
            }         
            return;
        }
        else if (endUpSet)
        {

            if (!playerController.completeWallRaiseUp)
            {
                characterController.Move(controllerTransform.TransformDirection(Vector3.up) * lastUpSpeed * Time.fixedDeltaTime);
                playerController.CheckRaiseUpWallCheck();
            }
            else
            {
                if (timer > 0.4f)
                {
                    playerController.exitWallState = true;
                    animator.SetBool("EndUpClimbing", false);

                }
                else
                {
                    characterController.Move(controllerTransform.TransformDirection(Vector3.forward) * lastPushPower * Time.fixedDeltaTime);
                    timer += Time.fixedDeltaTime;
                }
            }

            return;
        }
        else if (endDownSet)
        {
            if (Physics.Raycast(wallDirRayStartPos.position, wallDirRayStartPos.forward, out hitinfo, 0.4f))
            {
                controllerTransform.rotation = Quaternion.LookRotation(-hitinfo.normal);

            }

            characterController.Move(controllerTransform.TransformDirection(Vector3.down) * firstUpSpeed * Time.fixedDeltaTime);

            timer += Time.fixedDeltaTime;
            if (timer > 0.5f)
            {
                //targetRotation = Quaternion.LookRotation(-hitinfo.normal);
                //playerRoation = Quaternion.Slerp(controllerTransform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
                playerController.exitWallState = true;
                animator.SetBool("EndDownClimbing", false);
            }
            return;
        }


        playerController.CheckFrontWall();
   
        if (playerController.MoveDir != Vector3.zero)
        {

            playerController.StaminaConsumption(staminaMinus);

            if (playerController.ClimbingMoveRotateHitVector != Vector3.zero)
            {
                if(lastMemoryClimbingMoveRotateHitVector != playerController.ClimbingMoveRotateHitVector)
                {
                    //timer = 0.0f;
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

         

         

            //if (playerController.MoveDir.z != 0 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            //{
            //    isLeftHandUp = !isLeftHandUp;
            //    animator.SetBool(HandChange_Hash, isLeftHandUp);
            //}

            if (playerController.MoveDir.z != 0 && playerController.MoveDir.x == 0 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
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

        //위로 올라갈때 콜리더로 바꿔야할듯 문제는 여기다
        if (playerController.MoveDir.z > 0.5f && !playerController.isWallHit)
        {
            timer = 0.0f;
            endUpSet = true;
            animator.SetBool("EndUpClimbing", true);
            return;
        }
        //아래로 내려갈때
        else if (playerController.MoveDir.z < 0.5f)
        {
            if (playerController.CheckDownGroundEnter())
            {
                endDownSet = true;
                animator.SetBool("EndDownClimbing", true);
                return;
            }
        }


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

    /// <summary>
    /// 조종 캐릭터를 바꾸면 조종하는 애니메이터를 교체해줘야함
    /// </summary>
    /// <param name="animator"></param>
    public void ChangeAnimator(Animator animator)
    {
        this.animator = animator;
    }
}
