using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAttackAnimationEvent : MonoBehaviour
{
    public PlayerController playerInputSyatem;
    public CharacterController characterController;


    CapsuleCollider attackCollider;
    bool attackMove = false;

    private void Awake()
    {
        playerInputSyatem = GetComponent<PlayerController>();
        characterController = GetComponent<CharacterController>();
    }


    ////������ �ݸ��� ���� ����
    //public void AttackAnimation(float attackForwardMoveSpeed, Vector3 moveTargetDir)
    //{
    //    characterController.Move(attackForwardMoveSpeed * Time.fixedDeltaTime * moveTargetDir);
    //}

    public void AttackColliderActive()
    {

        attackCollider.enabled = attackCollider.enabled ? false : true;

    }

    public void AttackColliderDisable()
    {
        attackCollider.enabled = false;
    }

    public void AttackMoveFlag()
    {

        attackMove = attackMove ? false : true;
    }
}

