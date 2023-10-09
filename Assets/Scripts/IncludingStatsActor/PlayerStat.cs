using player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStat : IncludingStatsActor
{

    public bool IsAlive
    {
        get => isAlive;
    }

    public PlayerController playerController;
    public CharacterController characterController;
    
    //Vector3 moveTargetDir;

    public AnimatorOverrideController animator;


    protected bool attackMove = false;

    //public Action attackMoveAction;

    //이 아래는 강공격을 사용하기 위한 변수
    //애니메이션 모션마다 길이 달라서 강공격 시작할 수 있는 타이밍활성화를 위해서
    protected bool startTimer = false;
    protected float powerAttackTimer = 0.0f;
    protected float powerMaxTime = 1.0f;

    //공격시 이동 속도
    protected float attackMoveSpeed = 3.0f;


    //무기 공격시 소환 용도
    public GameObject handWeapon;
    public GameObject backWeapon;

    private void Awake()
    {
        
    }



    protected virtual void Update()
    {
        if(startTimer)//강공격 어택 스테이트에서 하는게 좋아 보임 바꾸셈
        {
            powerAttackTimer += Time.deltaTime;
            if (powerAttackTimer > powerMaxTime)
            {
                startTimer = false;
                
                //파워 어택 돌입
            }
        }
    }

    public override void AttackMove(Vector3 movedir)
    {

    }


    public void CanNextAttackFlag()
    {
        playerController.canAttack = true;
        
    }

    public void AttackMoveFlag()
    {
        attackMove = attackMove ? false : true;
    }

    protected virtual void PowerAttack()
    {

    }
    public virtual void UltimateSkill()
    {

    }

    protected void ActiveWeapon()
    {
        if (backWeapon.activeSelf)
        {
            backWeapon.SetActive(false);
        }
        handWeapon.SetActive(true);
    }

    protected void InactiveWeapon()
    {
        handWeapon.SetActive(false);
        backWeapon.SetActive(true);
    }

    public void SettingSummonWeapon()
    {
        playerController.activeWeapon = ActiveWeapon;
        playerController.inactiveWeapon = InactiveWeapon;
    }


    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);
        playerController.ControlEnterState(11);
    }

    public void OnDamage(float damage, bool knockback)
    {
        base.OnDamage(damage);
        playerController.Knockback = knockback;
        playerController.ControlEnterState(11);
    }

    /// <summary>
    /// 애니메이션 이밴트로 작동중
    /// </summary>
    public void ExitHitAnim()
    {
        if(!IsAlive)
        {
            playerController.DieToAliveCharacterChange();
        }
        playerController.EnterDefalutGroundState();
    }

    protected override void Die()
    {
        isAlive = false;
        playerController.PlayerDieAnimatorParamater(isAlive);
    }
}
