using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    UpMaterial = 0,
    Sword,
    Important,
    Food,
    Artifact
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
    // 등급에 따른 아이템 드랍 확률 (단위: %)
    public Dictionary<ItemGrade, float> gradeDropChances = new Dictionary<ItemGrade, float>()
    {
       { ItemGrade.None, 0f },          // 등급 없음
       { ItemGrade.oneStar, 40f },      // 1성 아이템 드랍 확률: 40%
       { ItemGrade.twoStar, 25f },      // 2성 아이템 드랍 확률: 25%
       { ItemGrade.threeStar, 15f },    // 3성 아이템 드랍 확률: 15%
       { ItemGrade.fourStar, 10f },     // 4성 아이템 드랍 확률: 10%
       { ItemGrade.fiveStar, 5f }       // 5성 아이템 드랍 확률: 5%
    };
    public ItemGrade itemgrade;
    public ItemType itemType;
    public string named = "아이템";
    public Sprite icon = null;
    public int id = 0;


    public virtual ItemData Clone()
    {
        ItemData clone = Instantiate(this);
        clone.itemgrade = this.itemgrade;
        clone.itemType = this.itemType;
        clone.named = this.named;
        clone.icon = this.icon;
        clone.id = this.id;
        clone.isEquippedItem = this.isEquippedItem;
        clone.equippedByCharacterId = this.equippedByCharacterId;
        clone.CurrentSlot = this.CurrentSlot;
        clone.itemDescription = this.itemDescription;

        return clone;
    }

    [HideInInspector]
    public bool isEquippedItem;
    [HideInInspector]
    public int equippedByCharacterId = -1; // 아이템을 장착한 캐릭터의 ID. -1은 장착되지 않았음을 의미합니다.
    public WeaponSlot CurrentSlot { get; set; } // 현재 아이템이 위치한 슬롯.

    private void OnDisable()
    {
        isEquippedItem = false;
        equippedByCharacterId = -1; // 아이템이 비활성화될 때 장착 상태도 초기화합니다.
    }
    /// <summary>
    /// 아이템 설명
    /// </summary>
    public string itemDescription;

}