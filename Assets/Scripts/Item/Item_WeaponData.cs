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
    Bow,
    /// <summary>
    /// �庴��
    /// </summary>
    Generals,
    /// <summary>
    /// ����
    /// </summary>
    MagicBook
}
[CreateAssetMenu(menuName = "Inventory/Weapon", fileName = "Weapon", order = 0)]
public class Item_WeaponData : ItemData
{
    [Header("����� �ɷ�ġ�� ��޿� ���� ������ ������ ���� �մϴ�.")]
    float AttackminRange, AttackmaxRange;
    float DeffenceminRange, DeffencemaxRange;
    public float plusAttacked;
    public float plusDefed;
    public int level = 1;
    public void SetAbilities()
    {
        // ��޿� ���� �ּ�ġ�� �ִ�ġ ����
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
       
        plusAttacked = Random.Range(AttackminRange, AttackmaxRange);
        plusAttacked = Mathf.Round(plusAttacked * 100f) / 100f; // �Ҽ��� ��° �ڸ����� �ݿø�
        plusDefed = Random.Range(DeffenceminRange, DeffencemaxRange);
        plusDefed = Mathf.Round(plusDefed * 100f) / 100f; // �Ҽ��� ��° �ڸ����� �ݿø�
    }
    
    public WeaponType weaponType;
}