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
    Bow,
    /// <summary>
    /// 장병기
    /// </summary>
    Generals,
    /// <summary>
    /// 법구
    /// </summary>
    MagicBook
}
[CreateAssetMenu(menuName = "Inventory/Weapon", fileName = "Weapon", order = 0)]
public class Item_WeaponData : ItemData
{
    [Header("장비의 능력치는 등급에 따라 랜덤한 난수를 세팅 합니다.")]
    float AttackminRange, AttackmaxRange;
    float DeffenceminRange, DeffencemaxRange;
    public float plusAttack;
    public float plusDef;
    public float upgradeAttack;
    public float upgradeDef;
    public int level = 1;
    public int dismantleOreAmount = 10; // 분해 시 반환되는 광석의 기본 양

    public void SetAbilities()
    {
        // 등급에 따라 최소치와 최대치 설정
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
        plusAttack = Mathf.Round(plusAttack * 100f) / 100f; // 소수점 둘째 자리까지 반올림
        plusDef = Random.Range(DeffenceminRange, DeffencemaxRange);
        plusDef = Mathf.Round(plusDef * 100f) / 100f; // 소수점 둘째 자리까지 반올림
    }
    public void Upgrade()
    {
        level++;
        plusAttack += upgradeAttack;
        plusDef += upgradeDef;
        if (level % 10 == 0)
        {
            Promote(); // 레벨이 10의 배수가 되면 진급합니다.
        }
    }
    private void Promote()
    {
        if (itemgrade == ItemGrade.fiveStar)
        {
            Debug.Log("이미 최고 등급입니다. 더 이상 진급할 수 없습니다.");
            return;
        }

        itemgrade++; // 등급을 한 단계 올립니다.
        Debug.Log("무기가 진급되었습니다! 현재 등급: " + itemgrade);
    }
    public void Dismantle(int oreAmount)
    {
        Debug.Log("무기를 분해합니다.");
        oreAmount = dismantleOreAmount + level;
    }
    public WeaponType weaponType;
}