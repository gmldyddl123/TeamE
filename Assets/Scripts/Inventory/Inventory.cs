using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Inventory : TestBase
{
    public ItemData item1;

    public static Inventory instance;
    public List<ItemData> exItems = new List<ItemData>();
    public List<ItemData> eqItems = new List<ItemData>();
    public List<ItemData> imItems = new List<ItemData>();
    public Item_WeaponData weaponitem;
    PlayerInputAction inputActions;

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
        inputActions.ItemTest.Enable();
        inputActions.ItemTest.Test1.performed += OnTest1;
        inputActions.ItemTest.Test2.performed += OnTest2;
        inputActions.ItemTest.Test3.performed += OnTest3;
        inputActions.ItemTest.Test4.performed += OnTest4;
        inputActions.ItemTest.Test5.performed += OnTest5;
        instance = this;
    }

    private void OnTest1(InputAction.CallbackContext obj)
    {
        Debug.Log("아이템 들어옴");
        //ItemDaata를 상속 받는 Item_WeaponDatad의 인스턴스 생성
        //이러면 Item_WeaponData클래스의 인스턴스들이 각각 독립적으로 생성
        Item_WeaponData newItem = ScriptableObject.CreateInstance<Item_WeaponData>();
        newItem.itemgrade = item1.itemgrade;
        newItem.itemType = item1.itemType;
        newItem.named = item1.named;
        newItem.icon = item1.icon;
        this.Add(newItem);
    }
    private void OnTest5(InputAction.CallbackContext context)
    {
        
    }

    private void OnTest4(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    private void OnTest3(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    private void OnTest2(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
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

        // 장착 상태에 따라 eqItems를 정렬합니다.
        eqItems = eqItems.OrderByDescending(item => item.isEquippedItem).ToList();

        // 정렬된 eqItems 리스트의 각 아이템에 대해 이벤트를 호출합니다.
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
                weaponitem.SetAbilities(); //랜덤한 능력치 설정
        }
    }
    public void OnSortTap()
    {
        onsortTap = !onsortTap;
        sortTap.gameObject.SetActive(onsortTap);
    }
}