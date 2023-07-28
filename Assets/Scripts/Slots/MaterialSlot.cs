using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class MaterialSlot : SlotData
{
    public TextMeshProUGUI current;
    public override void AddItem(ItemData newitem)
    {
        base.AddItem(newitem);
    }
    public override void SetSlotCount(int _count)
    {
        if (itemCount <= 0)
        {
            ClearSloat();
        }
        if (itemCount == 0)
        {
            current.gameObject.SetActive(false);
        }
        else
        {
            current.gameObject.SetActive(true);
        }
        itemCount += _count;
        current.text = itemCount.ToString();

    }
    public override void ClearSloat()
    {
        base.ClearSloat();
        //ui.UpdateExSlots();
        if (item == null)
        {
            InventorUi.instance.ExRearrange();
            if (itemCount == 0)
            {
                current.gameObject.SetActive(false);
            }
        }
    }
}
