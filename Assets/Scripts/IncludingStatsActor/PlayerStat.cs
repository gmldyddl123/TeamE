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

    //이 아래는 강공격을 사용하기 위한 변수
    //애니메이션 모션마다 길이 달라서 강공격 시작할 수 있는 타이밍활성화를 위해서
    protected bool startTimer = false;
    protected float powerAttackTimer = 0.0f;
    protected float powerMaxTime = 1.0f;

    //공격시 이동 속도
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
            Debug.Log("잘 들어오나?");
            if (powerAttackTimer > powerMaxTime)
            {
                startTimer = false;
                
                //파워 어택 돌입
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
