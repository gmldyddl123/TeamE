using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeponUiSelectUI : MonoBehaviour
{
    WeaponSlot[] weaponSlots;
    public Transform WeaponSlotParents;

    private void Awake()
    {
        weaponSlots = WeaponSlotParents.GetComponentsInChildren<WeaponSlot>();
    }
    private void Start()
    {
        Inventory.instance.onSwordUpSlotItemChanged += SwordSlotUIUpdate; //장비 슬롯 함수 실행
    }
    public void EqRearrange()
    {
        for (int i = 0; i < weaponSlots.Length; i++)
        {
            if (weaponSlots[i].item == null)
            {
                // 현재 슬롯이 비어있으면 뒤에 있는 슬롯들을 앞으로 이동시킴
                for (int j = i + 1; j < weaponSlots.Length; j++)
                {
                    if (weaponSlots[j].item != null)
                    {
                        weaponSlots[i].AddItem(weaponSlots[j].item);
                        weaponSlots[j].ClearSloat();
                        break;
                    }
                }
            }
        }
    }
    void SwordSlotUIUpdate(ItemData _item)
    {
        for (int i = 0; i < weaponSlots.Length; i++)
        {
            if (!weaponSlots[i].initItem)
            {
                weaponSlots[i].AddItem(_item);
                return;
            }
        }
    }
}
