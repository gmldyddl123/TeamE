using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
        onClearslot?.Invoke();
        eqItems = eqItems.OrderBy(item => (int)item.itemgrade).ToList();
        foreach (ItemData item in eqItems)
        {
            onSwordItemChanged?.Invoke(item);
        }
        OnSortTap();
        InventorUi.instance.ChangeEquipWeapon();
    }
    public void SortInventoryByGradeDown()
    {
        onClearslot?.Invoke();
        eqItems = eqItems.OrderByDescending(item => (int)item.itemgrade).ToList();
        foreach (ItemData item in eqItems)
        {
            onSwordItemChanged?.Invoke(item);
        }
        OnSortTap();
        InventorUi.instance.ChangeEquipWeapon();
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
    public void RemoveOre(int itemId, int count)
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
            onExItemRemoved?.Invoke(item);
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