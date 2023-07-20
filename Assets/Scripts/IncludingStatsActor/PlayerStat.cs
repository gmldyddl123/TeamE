using player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerStat : IncludingStatsActor
{
    //public PlayerController playerController;
    public CharacterController characterController;
    public CapsuleCollider attackCollider;

    //Vector3 moveTargetDir;

    public AnimatorOverrideController animator;


    protected bool attackMove = false;

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

    public void AttackMoveFlag()
    {
        attackMove = attackMove ? false : true;
    }

    protected virtual void PowerAttack()
    {

    }
    protected virtual void UltimateSkill()
    {

    }


   
}
