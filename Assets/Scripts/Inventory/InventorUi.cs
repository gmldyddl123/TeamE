using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventorUi : MonoBehaviour
{ 
    MaterialSlot[] materialSlot; 
    WeaponSlot[] weaponSlots;
    ImportantSlot[] importantSlots;
    FoodSlot[] foodSlots;
    ArtifactSlot[] artifactSlots;
    
    Inventory inventory;
    public static InventorUi instance;
    public Transform WeaponSlotParents;
    public Transform MaterialSlotParents;
    public Transform FoodSlotParents;
    public Transform ArtifactSlotParents;
    public Transform ImportantParents;

    int _count = 1;

    private void Awake()
    {
        instance = this;
        inventory = Inventory.instance;
        weaponSlots = WeaponSlotParents.GetComponentsInChildren<WeaponSlot>();
        materialSlot = MaterialSlotParents.GetComponentsInChildren<MaterialSlot>();
        foodSlots = FoodSlotParents.GetComponentsInChildren<FoodSlot>();
        artifactSlots = ArtifactSlotParents.GetComponentsInChildren<ArtifactSlot>();
        importantSlots = ImportantParents.GetComponentsInChildren<ImportantSlot>();
    }

    private void Start()
    {
        inventory.onUpMaterialItemChanged += UpMaterialSlotUIUpdate; //소모품 슬롯 함수 실행
        inventory.onSwordItemChanged += SwordSlotUIUpdate; //장비 슬롯 함수 실행
        inventory.onFoodItemChanged += FoodSlotUIUpdate;
        inventory.onImportantItemChanged += ImportatntSlotUIUpdate;
        inventory.onArtifactItemChanged += ArtifactSlotUIUpdate;
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
    public void ExRearrange()
    {
        for (int i = 0; i < materialSlot.Length; i++)
        {
            if (materialSlot[i].item == null)
            {
                // 현재 슬롯이 비어있으면 뒤에 있는 슬롯들을 앞으로 이동시킴
                for (int j = i + 1; j < materialSlot.Length; j++)
                {
                    if (materialSlot[j].item != null)
                    {
                        // 아이템 이동
                        materialSlot[i].AddItem(materialSlot[j].item);

                        // 아이템 카운트 유지
                        int tempItemCount = materialSlot[j].itemCount;
                        materialSlot[i].SetSlotCount(tempItemCount);

                        materialSlot[j].ClearSloat();
                        break;
                    }
                }
            }
        }
    }
    void UpMaterialSlotUIUpdate(ItemData _item)
    {
        for (int i = 0; i < materialSlot.Length; i++)
        {
            if (materialSlot[i].initItem)  // null 이라면 slots[i].item.itemName 할 때 런타임 에러 나서
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
    void FoodSlotUIUpdate(ItemData _item)
    {
        for (int i = 0; i < foodSlots.Length; i++)
        {
            if (foodSlots[i].initItem)  // null 이라면 slots[i].item.itemName 할 때 런타임 에러 나서
            {
                if (foodSlots[i].item.id == _item.id)
                {
                    foodSlots[i].SetSlotCount(_count);
                    return;
                }
            }
        }
        for (int i = 0; i < foodSlots.Length; i++)
        {
            if (!foodSlots[i].initItem)
            {
                foodSlots[i].AddItem(_item);
                foodSlots[i].SetSlotCount(_count);
                return;
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
    void ImportatntSlotUIUpdate(ItemData _item)
    {
        for (int i = 0; i < importantSlots.Length; i++)
        {
            if (!importantSlots[i].initItem)
            {
                importantSlots[i].AddItem(_item);
                return;
            }
        }
    }
    void ArtifactSlotUIUpdate(ItemData _item)
    {
        for (int i = 0; i < artifactSlots.Length; i++)
        {
            if (!artifactSlots[i].initItem)
            {
                artifactSlots[i].AddItem(_item);
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