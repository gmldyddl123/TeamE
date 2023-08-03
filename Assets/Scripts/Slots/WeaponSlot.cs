using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class WeaponSlot : SlotData
{
    public int weaponLevel;

    public GameObject isEquippedTap;

    public TextMeshProUGUI isEquippedText;

    public TextMeshProUGUI levelText;

    
    /// <summary>
    /// ������ ���� �������� �ƴ���
    /// </summary>
    public bool isEquippedSlot = false;
    public override void AddItem(ItemData newitem)
    {
        base.AddItem(newitem);
        levelText.text = "LV." + newitem.level;
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