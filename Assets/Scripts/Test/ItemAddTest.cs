using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class ItemAddTest : TestBase
{
    [Header("테스트용 아이템")]
    public ItemData item1;

    public ItemData item2;
    /// <summary>
    /// 테스트용 인벤토리
    /// </summary>
    public Inventory inventory;
    PlayerInputAction inputActions;
    public System.Action OnItemDrop;
    private void Awake()
    {
        inputActions = new PlayerInputAction();

    }
    
    private void OnTest1(InputAction.CallbackContext obj)
    {
        OnItemDrop?.Invoke();
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
        Debug.Log("아이템 들어옴");
        //ItemDaata를 상속 받는 Item_WeaponDatad의 인스턴스 생성
        //이러면 Item_WeaponData클래스의 인스턴스들이 각각 독립적으로 생성
        Item_UpMaterial newItem = ScriptableObject.CreateInstance<Item_UpMaterial>();
        newItem.itemgrade = item2.itemgrade;
        newItem.itemType = item2.itemType;
        newItem.named = item2.named;
        newItem.icon = item2.icon;
        inventory.Add(newItem);
    }

    private void OnTest2(InputAction.CallbackContext context)
    {
        Debug.Log("아이템 들어옴");
        //ItemDaata를 상속 받는 Item_WeaponDatad의 인스턴스 생성
        //이러면 Item_WeaponData클래스의 인스턴스들이 각각 독립적으로 생성
        Item_FoodItem newItem = ScriptableObject.CreateInstance<Item_FoodItem>();
        newItem.itemgrade = item1.itemgrade;
        newItem.itemType = item1.itemType;
        newItem.named = item1.named;
        newItem.icon = item1.icon;
        inventory.Add(newItem);
    }
}
