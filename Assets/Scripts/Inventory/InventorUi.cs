using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventorUi : MonoBehaviour
{ 
    MaterialSlot[] materialSlot; 
    WeaponSlot[] weaponSlots;
    Inventory inventory;
    public static InventorUi instance;
    public Transform WeaponSlotParents;
    public Transform MaterialSlotParents;

    int _count = 1;

    private void Awake()
    {
        instance = this;
        inventory = Inventory.instance;
        weaponSlots = WeaponSlotParents.GetComponentsInChildren<WeaponSlot>();
        materialSlot = MaterialSlotParents.GetComponentsInChildren<MaterialSlot>();
    }

    private void Start()
    {
        inventory.onExItemChanged += ExSlotUIUpdate; //�Ҹ�ǰ ���� �Լ� ����
        inventory.onEqItemChanged += EqSlotUIUpdate; //��� ���� �Լ� ����
        inventory.onClearslot += ClearAllSlots;
    }

    private void ClearAllSlots()
    {
        foreach (MaterialSlot slot in materialSlot)
        {
            slot.ClearSloat();
        }

        foreach (WeaponSlot slot in weaponSlots)
        {
            slot.ClearSloat();
        }
    }

    public void EqRearrange()
    {
        for (int i = 0; i < weaponSlots.Length; i++)
        {
            if (weaponSlots[i].item == null)
            {
                // ���� ������ ��������� �ڿ� �ִ� ���Ե��� ������ �̵���Ŵ
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
    public void ExRearrange()
    {
        for (int i = 0; i < materialSlot.Length; i++)
        {
            if (materialSlot[i].item == null)
            {
                // ���� ������ ��������� �ڿ� �ִ� ���Ե��� ������ �̵���Ŵ
                for (int j = i + 1; j < materialSlot.Length; j++)
                {
                    if (materialSlot[j].item != null)
                    {
                        // ������ �̵�
                        materialSlot[i].AddItem(materialSlot[j].item);

                        // ������ ī��Ʈ ����
                        int tempItemCount = materialSlot[j].itemCount;
                        materialSlot[i].SetSlotCount(tempItemCount);

                        materialSlot[j].ClearSloat();
                        break;
                    }
                }
            }
        }
    }
    void ExSlotUIUpdate(ItemData _item)
    {
        for (int i = 0; i < materialSlot.Length; i++)
        {
            if (materialSlot[i].initItem)  // null �̶�� slots[i].item.itemName �� �� ��Ÿ�� ���� ����
            {
                if (materialSlot[i].item.id == _item.id)
                {
                    materialSlot[i].SetSlotCount(_count);
                    return;
                }
            }
        }
        for (int i = 0; i < materialSlot.Length; i++)
        {
            if (!materialSlot[i].initItem)
            {
                materialSlot[i].AddItem(_item);
                materialSlot[i].SetSlotCount(_count);
                return;
            }
        }
    }
    void EqSlotUIUpdate(ItemData _item)
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
    public void ChangeEquipWeapon()
    {
        for (int i = 0; i < inventory.eqItems.Count; i++)
        {
            if (weaponSlots[i].item.isEquippedItem)
            {
                weaponSlots[i].isEquippedTap.gameObject.SetActive(true);
            }
            if (!weaponSlots[i].item.isEquippedItem)
            {
               weaponSlots[i].isEquippedTap.gameObject.SetActive(false);
            }
        }
    }
}