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
    /// 레벨
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
    /// 현재 장착 무기
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


    //공격 콤보 공격력 계수
    protected int comboCount = 0;
    protected int maxComboCount = 0;

    protected int[] comboDamage;


    protected bool attackMove = false;


    /// <summary>
    /// 스킬이팩트
    /// </summary>
    protected ParticleSystem skillEffect;

    protected Collider skillCollider;

    //public Action attackMoveAction;

    //이 아래는 강공격을 사용하기 위한 변수
    //애니메이션 모션마다 길이 달라서 강공격 시작할 수 있는 타이밍활성화를 위해서
    //현재는 사용이 되고있지 않다
    protected bool startTimer = false;
    protected float powerAttackTimer = 0.0f;
    protected float powerMaxTime = 1.0f;

    //공격시 이동 속도
    protected float attackMoveSpeed = 3.0f;


    //무기 공격시 소환 용도
    public GameObject handWeapon;
    public GameObject backWeapon;



    /// <summary>
    /// 회피 관련
    /// </summary>

    protected int dodgeMaxCount = 2; 
    protected int dodgeCount = 0;
    
    bool dodgeSuccess = false;    
    bool dodgeSituation = false;



    float invincibilityRemovalTime = 0.45f;
    float dodgeRecoveryTime = 1.0f;

    float dodgeSituationecoveryTime = 0.3f;

    protected virtual void Awake()
    {
        comboDamage = new int[maxComboCount];
        onHealthChange = FindObjectOfType<HealthBar>().PublicOnValueChange;
        Debug.Log(onHealthChange);
        //PublicOnValueChange
    }



    //protected virtual void Update()
    //{
    //    if(startTimer)//강공격 어택 스테이트에서 하는게 좋아 보임 바꾸셈
    //    {
    //        powerAttackTimer += Time.deltaTime;
    //        if (powerAttackTimer > powerMaxTime)
    //        {
    //            startTimer = false;
                
    //            //파워 어택 돌입
    //        }
    //    }
    //}

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
                result = CalculatedAttackPower;
                break;
            case 1:
                result = CalculatedAttackPower;
                break;
            case 10:
                result = CalculatedAttackPower * 3;
                break;
            default:
                result = CalculatedAttackPower;
                break;
        }
        return result;
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
        if(isAlive && !dodgeSuccess)
        {
            base.OnDamage(damage);
            playerController.ControlEnterState(11);
        }
    }

    public override void OnDamage(float damage, bool knockback, Vector3 attackPos)
    {
        if(isAlive && !dodgeSuccess)
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
    /// 애니메이션 이밴트로 작동중
    /// </summary>
    /// 


    public void SkillEffectOn()
    {
        //skillEffect.SetActive(skillEffect.activeSelf ? false : true); 
        skillEffect.Play();
    }
    public void SkillColliderActive()
    {
        comboCount = 10;
        skillCollider.enabled = !skillCollider.enabled;

    }


    public void ExitSkillState()
    {
        comboCount = 0;        
        playerController.PlayerEnterIdleState();
        InactiveWeapon();
        skillCollider.enabled = false;
        playerController.StopInputKey(true);
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
