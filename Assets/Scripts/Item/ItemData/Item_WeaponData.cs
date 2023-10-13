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
    float DeffenceminRange, DeffencemaxRange;
    public float plusAttack;
    public float plusDef;
    public float upgradeAttack;
    public float upgradeDef;
    public int level = 1;
    public int maxLevel = 50; // �ִ� ����
    public float exp = 0f; // ���� ����ġ
    public float maxExp = 100f; // �ִ� ����ġ

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
        // �ٸ� �ʵ尡 �ִٸ� ���⿡ �߰�...

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
       
        plusAttack = Random.Range(AttackminRange, AttackmaxRange);
        plusAttack = Mathf.Round(plusAttack * 100f) / 100f; 
        plusDef = Random.Range(DeffenceminRange, DeffencemaxRange);
        plusDef = Mathf.Round(plusDef * 100f) / 100f; 
    }
    public WeaponType weaponType;
}