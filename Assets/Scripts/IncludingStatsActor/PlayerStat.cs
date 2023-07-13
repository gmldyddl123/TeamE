using player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerStat : IncludingStatsActor
{
    public PlayerInputSystem playerInputSystem;
    public CharacterController characterController;
    public CapsuleCollider attackCollider;

    //Vector3 moveTargetDir;

    public AnimatorOverrideController animator;


    //public Action attackMoveAction;

    //�� �Ʒ��� �������� ����ϱ� ���� ����
    //�ִϸ��̼� ��Ǹ��� ���� �޶� ������ ������ �� �ִ� Ÿ�̹�Ȱ��ȭ�� ���ؼ�
    protected bool startTimer = false;
    protected float powerAttackTimer = 0.0f;
    protected float powerMaxTime = 1.0f;

    //���ݽ� �̵� �ӵ�
    protected float attackMoveSpeed = 3.0f;

    private void Awake()
    {
        Debug.Log("1");
        playerInputSystem = GetComponentInParent<PlayerInputSystem>();
        characterController = GetComponentInParent<CharacterController>();
        Debug.Log("2");
    }



    protected virtual void Update()
    {
        if(startTimer)
        {
            powerAttackTimer += Time.deltaTime;
            Debug.Log("�� ������?");
            if (powerAttackTimer > powerMaxTime)
            {
                startTimer = false;
                
                //�Ŀ� ���� ����
            }
        }
    }

    public override void Attack(Vector3 movedir)
    {
        startTimer = true;
        powerAttackTimer = 0f;

        //characterController.Move(
        //     Vector3.down * 3.0f
        //     * Time.fixedDeltaTime);
        //if (playerInputSystem.attackMove)
        //{
        //    //playerInputSystem.UseGravity();
        //    characterController.Move(attackMoveSpeed * Time.fixedDeltaTime * movedir);
        //}

    }

    protected virtual void PowerAttack()
    {

    }
    protected virtual void UltimateSkill()
    {

    }


   
}
