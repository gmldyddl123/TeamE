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
    public static Inventory instance;
    public List<ItemData> exItems = new List<ItemData>();
    public List<ItemData> eqItems = new List<ItemData>();
    public List<ItemData> imItems = new List<ItemData>();
    public Item_WeaponData weaponitem;

    public delegate void OnItemChanged(ItemData _item);

    public OnItemChanged onEqItemChanged;
    public OnItemChanged onExItemChanged;

    public Action onClearslot;

    public GameObject inven;
    public GameObject sortTap;

    bool activeInven = false;
    bool onsortTap = false;

    private void Start()
    {
        inven.SetActive(activeInven);
    }
    void Awake()
    {
        instance = this;
    }
    public void SortInventoryByGrade()
    {
        //exItems = exItems.OrderBy(item => (int)item.itemgrade).ToList();

        //foreach (ItemData item in exItems)
        //{
        //    onExItemChanged?.Invoke(item);
        //}
    }
    public void SortInventoryByGradeUp()
    {
        onClearslot?.Invoke();
        eqItems = eqItems.OrderBy(item => (int)item.itemgrade).ToList();
        foreach (ItemData item in eqItems)
        {
            onEqItemChanged?.Invoke(item);
        }
        sortTap.gameObject.SetActive(false);
        InventorUi.instance.ChangeEquipWeapon();
    }
    public void SortInventoryByGradeDown()
    {
        onClearslot?.Invoke();
        eqItems = eqItems.OrderByDescending(item => (int)item.itemgrade).ToList();
        foreach (ItemData item in eqItems)
        {
            onEqItemChanged?.Invoke(item);
        }
        sortTap.gameObject.SetActive(false);
        InventorUi.instance.ChangeEquipWeapon();
    }
    public void SortInventoryByName()
    {
        onClearslot?.Invoke();
        eqItems = eqItems.OrderBy(item => item.named).ToList();
        foreach (ItemData item in eqItems)
        {
            onEqItemChanged?.Invoke(item);
        }
        sortTap.gameObject.SetActive(false);
        InventorUi.instance.ChangeEquipWeapon();
    }
    public void SortInventoryByEquipped()
    {
        onClearslot?.Invoke();

        // ���� ���¿� ���� eqItems�� �����մϴ�.
        eqItems = eqItems.OrderByDescending(item => item.isEquippedItem).ToList();

        // ���ĵ� eqItems ����Ʈ�� �� �����ۿ� ���� �̺�Ʈ�� ȣ���մϴ�.
        foreach (ItemData item in eqItems)
        {
            onEqItemChanged?.Invoke(item);
        }
        sortTap.gameObject.SetActive(false);
        InventorUi.instance.ChangeEquipWeapon();
    }

    public void Add(ItemData addedItem)
    {
        if (addedItem.itemType == ItemType.expendables)
        {
            exItems.Add(addedItem);
            onExItemChanged?.Invoke(addedItem);
        }
        else if (addedItem.itemType == ItemType.equipment)
        {
            eqItems.Add(addedItem);
            onEqItemChanged?.Invoke(addedItem);
            if (addedItem is Item_WeaponData weaponitem)
                weaponitem.SetAbilities(); //������ �ɷ�ġ ����
        }
    }
    public void OnSortTap()
    {
        onsortTap = !onsortTap;
        sortTap.gameObject.SetActive(onsortTap);
    }
}