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
    /// 아이템이 등급에 따라 부여받은 별 딕셔너리
    /// </summary>
    public Dictionary<ItemGrade, string> gradeToStars = new Dictionary<ItemGrade, string>()
    {
        { ItemGrade.None, "" },
        { ItemGrade.oneStar, "★" },
        { ItemGrade.twoStar, "★★" },
        { ItemGrade.threeStar, "★★★" },
        { ItemGrade.fourStar, "★★★★" },
        { ItemGrade.fiveStar, "★★★★★" }
    };
    public Dictionary<ItemGrade, Color> gradeColor = new Dictionary<ItemGrade, Color>()
    {
        { ItemGrade.None, new Color(203f / 255f, 203f / 255f, 203f / 255f) },
        { ItemGrade.oneStar, new Color(203f / 255f, 203f / 255f, 203f / 255f) },
        { ItemGrade.twoStar, new Color(101f / 255f, 173f / 255f, 87f / 255f) },
        { ItemGrade.threeStar, new Color(66f / 255f, 185f / 255f, 182f / 255f) },
        { ItemGrade.fourStar, new Color(193f / 255f, 112f / 255f, 191f / 255f) },
        { ItemGrade.fiveStar, new Color(224f / 255f, 196f / 255f, 36f / 255f) }
    };
 
    public ItemGrade itemgrade;
    public ItemType itemType;
    public string named = "아이템";
    public Sprite icon = null;
    public int id = 0;
    //public WeaponSlot weaponSlot;
    public bool isEquippedItem = false;
    private void OnDisable()
    {
        isEquippedItem = false;
    }
    /// <summary>
    /// 아이템 설명
    /// </summary>
    public string itemDescription;
  
}