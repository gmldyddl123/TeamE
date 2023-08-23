using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class ItemAddTest : TestBase
{
    [Header("�׽�Ʈ�� ������")]
    public ItemData item1;

    public ItemData item2;
    /// <summary>
    /// �׽�Ʈ�� �κ��丮
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
        Debug.Log("������ ����");
        //ItemDaata�� ��� �޴� Item_WeaponDatad�� �ν��Ͻ� ����
        //�̷��� Item_WeaponDataŬ������ �ν��Ͻ����� ���� ���������� ����
        Item_UpMaterial newItem = ScriptableObject.CreateInstance<Item_UpMaterial>();
        newItem.itemgrade = item2.itemgrade;
        newItem.itemType = item2.itemType;
        newItem.named = item2.named;
        newItem.icon = item2.icon;
        inventory.Add(newItem);
    }

    private void OnTest2(InputAction.CallbackContext context)
    {
        Debug.Log("������ ����");
        //ItemDaata�� ��� �޴� Item_WeaponDatad�� �ν��Ͻ� ����
        //�̷��� Item_WeaponDataŬ������ �ν��Ͻ����� ���� ���������� ����
        Item_FoodItem newItem = ScriptableObject.CreateInstance<Item_FoodItem>();
        newItem.itemgrade = item1.itemgrade;
        newItem.itemType = item1.itemType;
        newItem.named = item1.named;
        newItem.icon = item1.icon;
        inventory.Add(newItem);
    }
}
