using player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStat : IncludingStatsActor
{


    protected string playerName = "";

    public string PlayerName { get => playerName; }

    public bool IsAlive
    {
        get => isAlive;
    }

    public PlayerController playerController;
    public CharacterController characterController;
    
    //Vector3 moveTargetDir; 

    public AnimatorOverrideController animator;


    //���� �޺� ���ݷ� ���
    protected int comboCount = 0;
    protected int maxComboCount = 0;

    protected int[] comboDamage;


    protected bool attackMove = false;


    //public Action attackMoveAction;

    //�� �Ʒ��� �������� ����ϱ� ���� ����
    //�ִϸ��̼� ��Ǹ��� ���� �޶� ������ ������ �� �ִ� Ÿ�̹�Ȱ��ȭ�� ���ؼ�
    protected bool startTimer = false;
    protected float powerAttackTimer = 0.0f;
    protected float powerMaxTime = 1.0f;

    //���ݽ� �̵� �ӵ�
    protected float attackMoveSpeed = 3.0f;


    //���� ���ݽ� ��ȯ �뵵
    public GameObject handWeapon;
    public GameObject backWeapon;

    protected virtual void Awake()
    {
        comboDamage = new int[maxComboCount];
        onHealthChange = FindObjectOfType<HealthBar>().PublicOnValueChange;
        Debug.Log(onHealthChange);
        //PublicOnValueChange
    }



    protected virtual void Update()
    {
        if(startTimer)//������ ���� ������Ʈ���� �ϴ°� ���� ���� �ٲټ�
        {
            powerAttackTimer += Time.deltaTime;
            if (powerAttackTimer > powerMaxTime)
            {
                startTimer = false;
                
                //�Ŀ� ���� ����
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


    protected float EnemyTargetDamage()
    {
        float result = 0;
        switch (comboCount)
        {
            case 0:
                result = ATK;
                break;
            case 1:
                result = ATK;
                break;
            case 10:
                result = ATK * 3;
                break;
            default:
                result = ATK;
                break;
        }
        return result;
    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);
        playerController.ControlEnterState(11);
    }

    public override void OnDamage(float damage, bool knockback, Vector3 attackPos)
    {
        base.OnDamage(damage);
        //playerController.Knockback = knockback;
        if(HP > 0)
        {
            playerController.ControlEnterState(11, knockback, attackPos);
        }
        else
        {
            playerController.ControlEnterState(11);
        }
    }

    /// <summary>
    /// �ִϸ��̼� �̹�Ʈ�� �۵���
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
