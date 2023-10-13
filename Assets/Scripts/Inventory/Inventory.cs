using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    private int money = 0;
    public int Money
    {
        get { return money; }
        set { money = value; }
    }
    public static Inventory instance;
    public List<ItemData> exItems = new List<ItemData>();
    public List<ItemData> eqItems = new List<ItemData>();
    public List<ItemData> imItems = new List<ItemData>();

    public delegate void OnItemChanged(ItemData _item);

    public OnItemChanged onSwordUpSlotItemChanged;
    public OnItemChanged onSwordItemChanged;
    public OnItemChanged onUpMaterialItemChanged;
    public OnItemChanged onFoodItemChanged;
    public OnItemChanged onImportantItemChanged;
    public OnItemChanged onArtifactItemChanged;
    public OnItemChanged onFoodItemRemoved;
    public OnItemChanged onExItemRemoved;

    public Action onClearslot;

    /// <summary>
    /// 인벤토리 탭
    /// </summary>
    public GameObject inventoryTap;

    public GameObject sort;
    public GameObject sortTap;

    public bool activeInven = false;

    public bool onsortTap = false;

    private void Start()
    {
        inventoryTap.SetActive(activeInven);
    }
    void Awake()
    {
        instance = this;
    }

    private void OnInven(InputAction.CallbackContext _)
    {
        activeInven = !activeInven;
        inventoryTap.SetActive(activeInven);
    }
    public void SortInventoryByGradeUp()
    {
        onClearslot?.Invoke(); // 모든 슬롯의 아이템을 클리어합니다.

        // 장착된 아이템 추적
        List<ItemData> equippedItems = eqItems.Where(item => item.isEquippedItem).ToList();

        // 아이템을 재정렬합니다.
        eqItems = eqItems.OrderBy(item => (int)item.itemgrade).ToList();
        // 모든 슬롯의 장착 상태를 해제합니다.
        foreach (var slot in InventorUi.instance.weaponSlots)
        {
            slot.isOneEquippedSlot = false;
            slot.isTwoEquippedSlot = false;
            slot.isEquippedTap.SetActive(false);
        }

        for (int i = 0; i < eqItems.Count; i++)
        {
            var item = eqItems[i];

            // 장착된 아이템이면 슬롯의 장착 상태를 true로 설정합니다.
            if (equippedItems.Contains(item))
            {
                var currentSlot = item.CurrentSlot;
                currentSlot.isOneEquippedSlot = true;
                currentSlot.isTwoEquippedSlot = true;
                currentSlot.isEquippedTap.SetActive(true); // 현재 슬롯의 장착 표시자를 활성화합니다.
                item.CurrentSlot = currentSlot; // 아이템의 현재 슬롯을 업데이트합니다.
            }

            onSwordItemChanged?.Invoke(item); // 필요한 경우 추가 이벤트 처리
        }

        InventorUi.instance.ChangeEquipWeapon(); // UI 업데이트
    }



    public void SortInventoryByGradeDown()
    {
        onClearslot?.Invoke(); // 모든 슬롯의 아이템을 클리어합니다.

        // 장착된 아이템 추적
        List<ItemData> equippedItems = eqItems.Where(item => item.isEquippedItem).ToList();

        eqItems = eqItems.OrderByDescending(item => (int)item.itemgrade).ToList();

        // 모든 슬롯의 장착 상태를 해제합니다.
        foreach (var slot in InventorUi.instance.weaponSlots)
        {
            slot.isOneEquippedSlot = false;
            slot.isTwoEquippedSlot = false;
            slot.isEquippedTap.SetActive(false);
        }

        for (int i = 0; i < eqItems.Count; i++)
        {
            var item = eqItems[i];

            // 장착된 아이템이면 슬롯의 장착 상태를 true로 설정합니다.
            if (equippedItems.Contains(item))
            {
                var currentSlot = item.CurrentSlot;
                currentSlot.isOneEquippedSlot = true;
                currentSlot.isTwoEquippedSlot = true;
                currentSlot.isEquippedTap.SetActive(true); // 현재 슬롯의 장착 표시자를 활성화합니다.
                item.CurrentSlot = currentSlot; // 아이템의 현재 슬롯을 업데이트합니다.
            }

            onSwordItemChanged?.Invoke(item); // 필요한 경우 추가 이벤트 처리
        }

        InventorUi.instance.ChangeEquipWeapon(); // UI 업데이트
    }
    public void SortInventoryByName()
    {
        onClearslot?.Invoke();
        eqItems = eqItems.OrderBy(item => item.named).ToList();
        foreach (ItemData item in eqItems)
        {
            onSwordItemChanged?.Invoke(item);
        }
        OnSortTap();
        InventorUi.instance.ChangeEquipWeapon();
    }
    public void SortInventoryByEquipped()
    {
        onClearslot?.Invoke();

        // 장착 상태에 따라 eqItems를 정렬합니다.
        eqItems = eqItems.OrderByDescending(item => item.isEquippedItem).ToList();

        // 정렬된 eqItems 리스트의 각 아이템에 대해 이벤트를 호출합니다.
        foreach (ItemData item in eqItems)
        {
            onSwordItemChanged?.Invoke(item);
        }
        OnSortTap();
        InventorUi.instance.ChangeEquipWeapon();
    }
    public void RemoveItems(int itemId, int count)
    {
        List<ItemData> itemsToRemove = new List<ItemData>();
        int removedCount = 0;

        foreach (ItemData item in exItems)
        {
            if (item.id == itemId)
            {
                removedCount++;
                itemsToRemove.Add(item);
                if (removedCount == count)
                {
                    break;
                }
            }
        }
        foreach (ItemData item in itemsToRemove)
        {
            exItems.Remove(item);
            if(item.itemType == ItemType.Food)
            {
                onFoodItemRemoved?.Invoke(item);
            }
            if (item.itemType == ItemType.UpMaterial)
            {
                onExItemRemoved?.Invoke(item);
            }
        }
    }

    public void Add(ItemData item)
    {
        if (item.itemType == ItemType.UpMaterial)
        {
            exItems.Add(item);
            onUpMaterialItemChanged?.Invoke(item);
        }
        else if (item.itemType == ItemType.Sword)
        {

            eqItems.Add(item);
            onSwordItemChanged?.Invoke(item);
            onSwordUpSlotItemChanged?.Invoke(item);
            if (item is Item_WeaponData weaponitem)
                weaponitem.SetAbilities(); //랜덤한 능력치 설정
        }
        if (item.itemType == ItemType.Food)
        {
            exItems.Add(item);
            onFoodItemChanged?.Invoke(item);
        }
        if (item.itemType == ItemType.Important)
        {
            exItems.Add(item);
            onImportantItemChanged?.Invoke(item);
        }
        if (item.itemType == ItemType.Artifact)
        {
            exItems.Add(item);
            onArtifactItemChanged?.Invoke(item);
        }
    }
    public void RemoveItem(ItemData itemToRemove)
    {
        exItems.Remove(itemToRemove);
        onFoodItemRemoved?.Invoke(itemToRemove);
    }
    public void OnSortTap()
    {
        onsortTap = !onsortTap;
        sortTap.gameObject.SetActive(onsortTap);
    }
   
    public int GetOneOreCount()
    {
        int count = 0;
        foreach (ItemData item in exItems)
        {
            if (item.id == 10)
            {
                count++;
            }
        }
        return count;
    }
    public int GetTwoOreCount()
    {
        int count = 0;
        foreach (ItemData item in exItems)
        {
            if (item.id == 11)
            {
                count++;
            }
        }
        return count;
    }
    public int GetOneThreesCount()
    {
        int count = 0;
        foreach (ItemData item in exItems)
        {
            if (item.id == 12)
            {
                count++;
            }
        }
        return count;
    }
}