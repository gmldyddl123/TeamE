using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{

    

    //���۳�Ʈ
    PlayerInputAction inputActions;
    Rigidbody playerRigidbody;
    CharacterController characterController;

    //���� ����
    PlayerState playerCurrentStates; // ���� ����
    PlayerState runState; //�޸���
    
    //�Է°�
    private Vector2 movementInput; //�׼����� �޴� �Է°�
    private Vector3 moveDir; //�Է°����� ���� ����3

    //ĳ���� ��Ʈ�ѷ�
    float gravity = -9.81f; // �߷�
    public Vector3 moveDirection; // ī�޶���� ����� �̵� ����

    //ȸ��
    Transform cameraObject;
    Vector3 targetDirection = Vector3.zero; //ȸ���ϴ� ����
    private float rotationSpeed = 7f;

    private void Awake()
    {
        inputActions = new PlayerInputAction();
        playerRigidbody = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
        cameraObject = Camera.main.transform;
    }

    private void OnEnable()
    {

        //��ǲ�ý���
        inputActions.Player.Enable();
        inputActions.Player.Movement.performed += MovementLogic;
        inputActions.Player.Movement.canceled += MovementLogic;

        //inputActions.Player.CameraLook.performed += i => cameraInput = i.ReadValue<Vector2>();

        //����
        runState = new RunState(this);
        playerCurrentStates = runState;
    }

    private void MovementLogic(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        moveDir.x = movementInput.x;
        moveDir.z = movementInput.y;
    }


    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    private void Update()
    {
 
    }
    private void FixedUpdate()
    {
        playerCurrentStates.MoveLogic();
    }


    public void PlayerMove(float moveSpeed)
    {
        if (characterController.isGrounded == false)
        {
            moveDirection.y += gravity * Time.fixedDeltaTime;
        }
        //else
        //{
        //    moveDirection.y = 0;
        //}
        characterController.Move(moveDirection * moveSpeed * Time.fixedDeltaTime);
    }


    public void MoveToDir()
    {
        Vector3 movedis = cameraObject.rotation * new Vector3(moveDir.x, 0 ,moveDir.z);

        moveDirection = new Vector3(movedis.x, moveDirection.y, movedis.z);
        PlayerRotate();
    }

    private void PlayerRotate()
    {
        targetDirection = cameraObject.forward * moveDir.z;
        targetDirection = targetDirection + cameraObject.right * moveDir.x;
        targetDirection.Normalize();

        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;

        targetDirection.y = 0;


        Quaternion targerRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRoation = Quaternion.Slerp(transform.rotation, targerRotation, rotationSpeed * Time.fixedDeltaTime);

        transform.rotation = playerRoation;
    }
}
