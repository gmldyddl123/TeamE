using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public enum WeaponType
{
    /// <summary>
    /// 검
    /// </summary>
    Sword,
    /// <summary>
    /// 활
    /// </summary>
    Bow
}

[CreateAssetMenu(menuName = "Inventory/Weapon", fileName = "Weapon", order = 0)]
public class Item_WeaponData : ItemData
{
    [Header("장비의 능력치는 등급에 따라 랜덤한 난수를 세팅 합니다.")]
    float AttackminRange, AttackmaxRange;
    float DeffenceminRange, DeffencemaxRange;
    public float _plusAttack;
    public float plusAttack
    {
        get => _plusAttack;
        set
        {
            if (_plusAttack != value)
            {
                _plusAttack = value;
                OnAttackChanged?.Invoke(_plusAttack); // 공격력이 변경되면 이벤트 발생
            }
        }
    }

    // 공격력이 변경될 때 호출될 이벤트
    public event Action<float> OnAttackChanged;
    public float plusDef;
    public float upgradeAttack;
    public float upgradeDef;
    public delegate void OnWeaponLevelChanged(Item_WeaponData weaponData);
    public event OnWeaponLevelChanged onWeaponLevelChanged;
    public int _level = 1;
    public int level
    {
        get => _level;
        set
        {
            if (_level != value)
            {
                _level = value;
                onWeaponLevelChanged?.Invoke(this); // 레벨이 변경되면 이벤트 발생
            }
        }
    }
    public int maxLevel = 50; // 최대 레벨
    public float exp = 0f; // 현재 경험치
    public float maxExp = 100f; // 최대 경험치

    public override ItemData Clone()
    {
        Item_WeaponData clone = Instantiate(this) as Item_WeaponData;
        clone.AttackminRange = this.AttackminRange;
        clone.AttackmaxRange = this.AttackmaxRange;
        clone.DeffenceminRange = this.DeffenceminRange;
        clone.DeffencemaxRange = this.DeffencemaxRange;
        clone.plusAttack = this.plusAttack;
        clone.plusDef = this.plusDef;
        clone.upgradeAttack = this.upgradeAttack;
        clone.upgradeDef = this.upgradeDef;
        clone.level = this.level;
        clone.maxLevel = this.maxLevel;
        clone.exp = this.exp;
        clone.maxExp = this.maxExp;
        clone.weaponType = this.weaponType;
        // 다른 필드가 있다면 여기에 추가...

        return clone;
    }
    public void SetAbilities()
    {
        switch (itemgrade)
        {
            case ItemGrade.oneStar:
                AttackminRange = 1f;
                AttackmaxRange = 11f;
                DeffenceminRange = 1f;
                DeffencemaxRange = 11f;
                break;

            case ItemGrade.twoStar:
                AttackminRange = 11f;
                AttackmaxRange = 21f;
                DeffenceminRange = 11f;
                DeffencemaxRange = 21f;
                break;

            case ItemGrade.threeStar:
                AttackminRange = 21f;
                AttackmaxRange = 31f;
                DeffenceminRange = 21f;
                DeffencemaxRange = 31f;
                break;
            case ItemGrade.fourStar:
                AttackminRange = 31f;
                AttackmaxRange = 41f;
                DeffenceminRange = 31f;
                DeffencemaxRange = 41f;
                break;

            case ItemGrade.fiveStar:
                AttackminRange = 41f;
                AttackmaxRange = 51f;
                DeffenceminRange = 41f;
                DeffencemaxRange = 51f;
                break;
            default:
                AttackminRange = 1f;
                AttackmaxRange = 1f;
                DeffenceminRange = 1f;
                DeffencemaxRange = 1f;
                break;
        }
       
        plusAttack = UnityEngine.Random.Range(AttackminRange, AttackmaxRange);
        plusAttack = Mathf.Round(plusAttack * 100f) / 100f; 
        plusDef = UnityEngine.Random.Range(DeffenceminRange, DeffencemaxRange);
        plusDef = Mathf.Round(plusDef * 100f) / 100f; 
    }
    public WeaponType weaponType;
}