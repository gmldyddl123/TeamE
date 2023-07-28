using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    expendables = 0,
    equipment,
    important,
}
public enum ItemGrade
{
    None = 0,
    oneStar,
    twoStar,
    threeStar,
    fourStar,
    fiveStar,
}

public class ItemData : ScriptableObject
{
    /// <summary>
    /// �������� ��޿� ���� �ο����� �� ��ųʸ�
    /// </summary>
    public Dictionary<ItemGrade, string> gradeToStars = new Dictionary<ItemGrade, string>()
    {
        { ItemGrade.None, "" },
        { ItemGrade.oneStar, "��" },
        { ItemGrade.twoStar, "�ڡ�" },
        { ItemGrade.threeStar, "�ڡڡ�" },
        { ItemGrade.fourStar, "�ڡڡڡ�" },
        { ItemGrade.fiveStar, "�ڡڡڡڡ�" }
    };
    /// <summary>
    /// rgb�� 010 ��Ÿ�Ϸ� �ٲٱ�
    /// </summary>
    public Dictionary<ItemGrade, Color> gradeColor = new Dictionary<ItemGrade, Color>()
    {
        { ItemGrade.None, new Color(203f / 255f, 203f / 255f, 203f / 255f) },
        { ItemGrade.oneStar, new Color(203f / 255f, 203f / 255f, 203f / 255f) },
        { ItemGrade.twoStar, new Color(101f / 255f, 173f / 255f, 87f / 255f) },
        { ItemGrade.threeStar, new Color(66f / 255f, 185f / 255f, 182f / 255f) },
        { ItemGrade.fourStar, new Color(193f / 255f, 112f / 255f, 191f / 255f) },
        { ItemGrade.fiveStar, new Color(224f / 255f, 196f / 255f, 36f / 255f) }
    };
    // ��޿� ���� ������ ��� Ȯ�� (����: %)
    public Dictionary<ItemGrade, float> gradeDropChances = new Dictionary<ItemGrade, float>()
    {
       { ItemGrade.None, 0f },          // ��� ����
       { ItemGrade.oneStar, 40f },      // 1�� ������ ��� Ȯ��: 40%
       { ItemGrade.twoStar, 25f },      // 2�� ������ ��� Ȯ��: 25%
       { ItemGrade.threeStar, 15f },    // 3�� ������ ��� Ȯ��: 15%
       { ItemGrade.fourStar, 10f },     // 4�� ������ ��� Ȯ��: 10%
       { ItemGrade.fiveStar, 5f }       // 5�� ������ ��� Ȯ��: 5%
    };

    public ItemGrade itemgrade;
    public ItemType itemType;
    public string named = "������";
    public Sprite icon = null;
    public int id = 0;
    public bool isEquippedItem = false;
    private void OnDisable()
    {
        isEquippedItem = false;
    }
    /// <summary>
    /// ������ ����
    /// </summary>
    public string itemDescription;
  
}