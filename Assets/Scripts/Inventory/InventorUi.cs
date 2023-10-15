using System;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventorUi : MonoBehaviour
{ 
    MaterialSlot[] materialSlot;
    public WeaponSlot[] weaponSlots; // 이제 이 배열은 public입니다.
    ImportantSlot[] importantSlots;
    FoodSlot[] foodSlots;
    
    Inventory inventory;
    public static InventorUi instance;
    public Transform WeaponSlotParents;
    public Transform MaterialSlotParents;
    public Transform FoodSlotParents;
    public Transform ImportantParents;
    Transform Inventap;
    bool isActive;
    int _count = 1;

    InventoryInputAction _action;

    private void Awake()
    {
        instance = this;
        inventory = Inventory.instance;
        _action = new ();
        Inventap = transform.GetChild(0).GetComponent<Transform>();
        weaponSlots = WeaponSlotParents.GetComponentsInChildren<WeaponSlot>();
        materialSlot = MaterialSlotParents.GetComponentsInChildren<MaterialSlot>();
        foodSlots = FoodSlotParents.GetComponentsInChildren<FoodSlot>();
        importantSlots = ImportantParents.GetComponentsInChildren<ImportantSlot>();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        inventory.onExItemRemoved += ItemRemoveExSlot;
        inventory.onFoodItemRemoved += ItemRemoveFoodSlot;
        inventory.onUpMaterialItemChanged += UpMaterialSlotUIUpdate; //소모품 슬롯 함수 실행
        inventory.onSwordItemChanged += SwordSlotUIUpdate; //장비 슬롯 함수 실행
        inventory.onFoodItemChanged += FoodSlotUIUpdate;
        inventory.onImportantItemChanged += ImportatntSlotUIUpdate;
        inventory.onClearslot += ClearAllSlots;
    }

    private void ItemRemoveExSlot(ItemData _item)
    {
        foreach (var slot in materialSlot)
        {
            if (slot.item != null && slot.item.id == _item.id)
            {
                slot.SetSlotCount(-_count);
                break;
            }
        }
    }

    private void ItemRemoveFoodSlot(ItemData _item)
    {
        foreach (var slot in foodSlots)
        {
            if (slot.item != null && slot.item.id == _item.id)
            {
                slot.SetSlotCount(-_count);
                break;
            }
        }
    }

    private void OnEnable()
    {
        _action.Inven.Enable();
        _action.Inven.OpenInven.performed += OnInven;
        _action.Inven.OpenInven.canceled += OnInven;
    }

    private void OnInven(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        // 키가 눌릴 때만 반응하도록 조건을 추가합니다.
        if (context.phase == UnityEngine.InputSystem.InputActionPhase.Performed)
        {
            // 'Inventap'의 현재 활성화 상태를 확인하고, 그와 반대 상태로 설정합니다.
            isActive = Inventap.gameObject.activeSelf;
            Inventap.gameObject.SetActive(!isActive);
        }
    }

    private void OnDisable()
    {
        _action.Inven.OpenInven.canceled -= OnInven;
        _action.Inven.OpenInven.performed -= OnInven;
        _action.Inven.Disable();
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