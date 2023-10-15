using Cinemachine;
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

    public Sprite portrait;


    /// <summary>
    /// ����
    /// </summary>
    protected int lv = 1;

    public int LV { get => lv; }

    protected float exp = 0;
    public float EXP
    {
        get => exp;
        set
        {
            if(exp != value)
            {
                exp = value;
            }

        }
    }


    /// <summary>
    /// ���� ���� ����
    /// </summary>
    Item_WeaponData equipWeapon;

    public Item_WeaponData EquipWeapon
    {
        get => equipWeapon;
        set
        {
            if (value != equipWeapon)
            {
                if(value == null)
                {
                    equipWeapon = value;
                    CalculatedAttackPower = atk;
                }
                else
                {
                    equipWeapon = value;
                    CalculatedAttackPower = atk + equipWeapon.plusAttack;
                }

              
            }
        }
    }



   
    public PlayerController playerController;
    public CharacterController characterController;
    
    //Vector3 moveTargetDir; 

    public AnimatorOverrideController animator;


    //���� �޺� ���ݷ� ���
    protected int attackCount = 0;

    public int AttackCount 
    {
        get => attackCount;
        set
        {
            if(attackCount != value)
            {
                attackCount = value;
            }
        }
    }

    protected int maxAttackCount = 0;

    protected float[] attackDamageCalculation;


    protected bool attackMove = false;


    /// <summary>
    /// ��ų����Ʈ
    /// </summary>
    protected ParticleSystem skillEffect;

    protected Collider skillCollider;

    protected CinemachineDollyCart skillCart;

    protected CinemachineVirtualCamera skillCutSceneCamera;

    //public Action attackMoveAction;

    //�� �Ʒ��� �������� ����ϱ� ���� ����
    //�ִϸ��̼� ��Ǹ��� ���� �޶� ������ ������ �� �ִ� Ÿ�̹�Ȱ��ȭ�� ���ؼ�
    //����� ����� �ǰ����� �ʴ�
    protected bool startTimer = false;
    protected float powerAttackTimer = 0.0f;
    protected float powerMaxTime = 1.0f;

    //���ݽ� �̵� �ӵ�
    protected float attackMoveSpeed = 3.0f;


    //���� ���ݽ� ��ȯ �뵵
    public GameObject handWeapon;
    public GameObject backWeapon;



    /// <summary>
    /// ȸ�� ����
    /// </summary>

    protected int dodgeMaxCount = 2; 
    protected int dodgeCount = 0;


    public bool invincible { get; set; } = false;
   

    bool dodgeSuccess = false;    
    bool dodgeSituation = false;



    float invincibilityRemovalTime = 0.45f;
    float dodgeRecoveryTime = 1.0f;

    float dodgeSituationecoveryTime = 0.3f;

    protected virtual void Awake()
    {
        attackDamageCalculation = new float[maxAttackCount];
        onHealthChange = FindObjectOfType<HealthBar>().PublicOnValueChange;
        
        Debug.Log(onHealthChange);
        //PublicOnValueChange
    }



    //protected virtual void Update()
    //{
    //    if(startTimer)//������ ���� ������Ʈ���� �ϴ°� ���� ���� �ٲټ�
    //    {
    //        powerAttackTimer += Time.deltaTime;
    //        if (powerAttackTimer > powerMaxTime)
    //        {
    //            startTimer = false;
                
    //            //�Ŀ� ���� ����
    //        }
    //    }
    //}

    public override void AttackMove(Vector3 movedir)
    {

    }


    public void CurrentAttackCountUp()
    {
        //attackCount++;
        Debug.Log(attackCount);
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
        Debug.Log($"������{calculatedAttackPower}, ���� �޺�{attackCount}");

        Debug.Log("��ų������ " + attackDamageCalculation[attackCount]);
        return calculatedAttackPower * attackDamageCalculation[attackCount];
    }


    public bool Dodge()
    {
        if(dodgeCount < dodgeMaxCount)
        {
            dodgeCount++;
            dodgeSuccess = true;
            StartCoroutine(DodgeReturnBool());

            if(!dodgeSituation)
            {
                dodgeSituation = true;
                StartCoroutine(DodgeSituation());
            }
            return true;
        }

        return false;
    }


    IEnumerator DodgeReturnBool()
    {
        yield return new WaitForSeconds(invincibilityRemovalTime);
        dodgeSuccess = false;
        yield return new WaitForSeconds(dodgeRecoveryTime - invincibilityRemovalTime);
        dodgeCount--;
    }

    IEnumerator DodgeSituation()
    {
        Time.timeScale = 0.65f;
        yield return new WaitForSeconds(dodgeSituationecoveryTime);
        Time.timeScale = 1f;
        dodgeSituation = false;
    }

    public override void OnDamage(float damage)
    {
        if (invincible) return;


        if (isAlive && !dodgeSuccess)
        {
            base.OnDamage(damage);
            playerController.ControlEnterState(11);
        }
    }

    public override void OnDamage(float damage, bool knockback, Vector3 attackPos)
    {
        if (invincible) return;

        if (isAlive && !dodgeSuccess)
        {
            base.OnDamage(damage);
            //playerController.Knockback = knockback;
            if (HP > 0)
            {
                playerController.ControlEnterState(11, knockback, attackPos);
            }
            else
            {
                playerController.ControlEnterState(11);
            }
        }
    }

    /// <summary>
    /// �ִϸ��̼� �̹�Ʈ�� �۵���
    /// </summary>
    /// 

    public virtual void SkillCameraOn()
    {

    }

    public void SkillEffectOn()
    {
        //skillEffect.SetActive(skillEffect.activeSelf ? false : true); 
        skillEffect.Play();
    }
    public void SkillColliderActive()
    {
        attackCount = maxAttackCount - 1;
        skillCollider.enabled = !skillCollider.enabled;

    }


    public void ExitSkillState()
    {
        attackCount = 0;        
        playerController.PlayerEnterIdleState();
        InactiveWeapon();
        skillCollider.enabled = false;
        playerController.StopInputKey(true);


        skillCart.m_Speed = 0;
        skillCart.m_Position = 0;

        invincible = false;
        skillCutSceneCamera.Priority = -1;
    }

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
