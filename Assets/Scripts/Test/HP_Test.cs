using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class HP_Test : TestBase
{
    public IncludingStatsActor state;
    public Inventory inventory;
    public ItemData item;
    protected override void Test1(InputAction.CallbackContext context)
    {
        state.HP -= 30f;
    }
    protected override void Test2(InputAction.CallbackContext context)
    {
        state.HP += 30f;
    }
    public PlayerController controller;

   
    protected override void Test4(InputAction.CallbackContext context)
    {
        Debug.Log("������ ����");
        //ItemDaata�� ��� �޴� Item_WeaponDatad�� �ν��Ͻ� ����
        //�̷��� Item_WeaponDataŬ������ �ν��Ͻ����� ���� ���������� ����
        inventory.Add(item);
    }
}
