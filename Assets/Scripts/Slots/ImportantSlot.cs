using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ImportantSlot : SlotData
{
    public TextMeshProUGUI current;
    public override void AddItem(ItemData newitem)
    {
        base.AddItem(newitem);
    }
    public override void SetSlotCount(int _count)
    {
        itemCount += _count;
        current.gameObject.SetActive(true);
        if (itemCount <= 0)
        {
            ClearSloat();
        }
        if (itemCount == 0)
        {
            current.gameObject.SetActive(false);
        }
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
    public void Set()
    {
        info.Get(item);
        newTap.gameObject.SetActive(false);
        newText.text = null;
    }
}
