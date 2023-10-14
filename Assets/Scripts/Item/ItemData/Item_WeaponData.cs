using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public enum WeaponType
{
    /// <summary>
    /// ��
    /// </summary>
    Sword,
    /// <summary>
    /// Ȱ
    /// </summary>
    Bow
}

[CreateAssetMenu(menuName = "Inventory/Weapon", fileName = "Weapon", order = 0)]
public class Item_WeaponData : ItemData
{
    [Header("����� �ɷ�ġ�� ��޿� ���� ������ ������ ���� �մϴ�.")]
    float AttackminRange, AttackmaxRange;
    public float plusAttack;
    public int level = 1;
    public int maxLevel = 50; // �ִ� ����
    public float exp = 0f; // ���� ����ġ
    public float maxExp = 100f; // �ִ� ����ġ

    public override ItemData Clone()
    {
        Item_WeaponData clone = Instantiate(this) as Item_WeaponData;
        clone.AttackminRange = this.AttackminRange;
        clone.AttackmaxRange = this.AttackmaxRange;
        clone.plusAttack = this.plusAttack;
        clone.level = this.level;
        clone.maxLevel = this.maxLevel;
        clone.exp = this.exp;
        clone.maxExp = this.maxExp;
        clone.weaponType = this.weaponType;
        return clone;
    }
    public void SetAbilities()
    {
        switch (itemgrade)
        {
            case ItemGrade.oneStar:
                AttackminRange = 1f;
                AttackmaxRange = 11f;
                break;

            case ItemGrade.twoStar:
                AttackminRange = 11f;
                AttackmaxRange = 21f;
                break;

            case ItemGrade.threeStar:
                AttackminRange = 21f;
                AttackmaxRange = 31f;
                break;
            case ItemGrade.fourStar:
                AttackminRange = 31f;
                AttackmaxRange = 41f;
                break;

            case ItemGrade.fiveStar:
                AttackminRange = 41f;
                AttackmaxRange = 51f;
                break;
            default:
                AttackminRange = 1f;
                AttackmaxRange = 1f;
                break;
        }
       
        plusAttack = Random.Range(AttackminRange, AttackmaxRange);
        plusAttack = Mathf.Round(plusAttack * 100f) / 100f;     }
    public WeaponType weaponType;
}