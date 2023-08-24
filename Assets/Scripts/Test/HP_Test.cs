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
        Debug.Log("아이템 들어옴");
        //ItemDaata를 상속 받는 Item_WeaponDatad의 인스턴스 생성
        //이러면 Item_WeaponData클래스의 인스턴스들이 각각 독립적으로 생성
        inventory.Add(item);
    }
}
