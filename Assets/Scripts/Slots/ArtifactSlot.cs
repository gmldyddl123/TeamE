using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArtifactSlot : SlotData
{
    public int weaponLevel;

    public GameObject isEquippedTap;

    public TextMeshProUGUI isEquippedText;

    public TextMeshProUGUI levelText;


    /// <summary>
    /// 슬롯이 장착 상태인지 아닌지
    /// </summary>
    public bool isEquippedSlot = false;
    public override void AddItem(ItemData newitem)
    {
        base.AddItem(newitem);
        if(newitem is Item_WeaponData weaponData)
        {
            levelText.text = "LV." + weaponData.level;
        }
        
    }
    public override void ClearSloat()
    {
        base.ClearSloat();
    }
    public void Set()
    {
        info.Get(item);
        newTap.gameObject.SetActive(false);
        newText.text = null;
    }
}
