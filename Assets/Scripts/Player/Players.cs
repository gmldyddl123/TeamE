using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class Players : MonoBehaviour
{
    [Header("테스트용 아이템")]
    public ItemData item1;
    public ItemData item2;
    public ItemData item3;
    public ItemData item4;
    public ItemData item5;
    /// <summary>
    /// 테스트용 인벤토리
    /// </summary>
    public Inventory inventory;

    public float speed = 5.0f;

    Vector3 dir = Vector3.zero;

    GameObject useCheck;
    
    private void Awake()
    {
        UseChecker checker = GetComponentInChildren<UseChecker>();
        useCheck = transform.GetChild(0).gameObject;
        //checker.onItemUse += UseItem;
    }

    //private void OnEnable()
    //{
    //    input.player.Enable();
    //    input.Test.Enable();
    //    input.Test.Test1.performed += Test1;
    //    input.Test.Test2.performed += Test2;
    //    input.Test.Test3.performed += Test3;
    //    input.Test.Test4.performed += Test4;
    //    input.Test.Test5.performed += Test5;
    //    input.player.Move.performed += OnMove;
    //    input.player.Move.canceled += OnMove;
    //    input.player.interactable.performed += OnInteractable;
    //}
    //private void OnDisable()
    //{
    //    input.player.interactable.performed -= OnMove;
    //    input.player.Move.canceled -= OnMove;
    //    input.player.Move.performed -= OnMove;
    //    input.Test.Test1.performed -= Test1;
    //    input.player.Disable();
    //}
    //private void Update()
    //{
    //    transform.Translate(Time.deltaTime * speed * dir);
    //}
    //private void OnMove(InputAction.CallbackContext context)
    //{
    //    Vector3 value = context.ReadValue<Vector3>();
    //    dir = value;
    //}
    //private void UseItem(IInteractable interactable)
    //{
    //    if (interactable.IsDirectUse)
    //    {
    //        interactable.Use();
    //        useCheck.GetComponent<CapsuleCollider>().enabled = false;
    //    }
    //}
    //private void OnInteractable(InputAction.CallbackContext _)
    //{
    //    useCheck.GetComponent<CapsuleCollider>().enabled = true;
    //}

    //private void DisInteractable(InputAction.CallbackContext context)
    //{
    //    useCheck.GetComponent<CapsuleCollider>().enabled = false;
    //}
    //public void Test1(InputAction.CallbackContext _)
    //{
    //    Debug.Log("아이템 들어옴");
    //    //ItemDaata를 상속 받는 Item_WeaponDatad의 인스턴스 생성
    //    //이러면 Item_WeaponData클래스의 인스턴스들이 각각 독립적으로 생성
    //    Item_UnMaterial newItem = ScriptableObject.CreateInstance<Item_UnMaterial>();
    //    newItem.itemgrade = item1.itemgrade;
    //    newItem.itemType = item1.itemType;
    //    newItem.named = item1.named;
    //    newItem.icon = item1.icon;
    //    inventory.Add(newItem);
    //}
    //public void Test2(InputAction.CallbackContext _)
    //{
    //    Item_WeaponData newItem = ScriptableObject.CreateInstance<Item_WeaponData>();
    //    newItem.itemgrade = item2.itemgrade;
    //    newItem.itemType = item2.itemType;
    //    newItem.named = item2.named;
    //    newItem.icon = item2.icon;
    //    Debug.Log("아이템 들어옴");
    //    inventory.Add(item2);
    //}
    //public void Test3(InputAction.CallbackContext _)
    //{
    //    Item_WeaponData newItem = ScriptableObject.CreateInstance<Item_WeaponData>();
    //    newItem.itemgrade = item3.itemgrade;
    //    newItem.itemType = item3.itemType;
    //    newItem.named = item3.named;
    //    newItem.icon = item3.icon;
    //    Debug.Log("아이템 들어옴");
    //    inventory.Add(item3);
    //}
    //public void Test4(InputAction.CallbackContext _)
    //{
    //    Item_WeaponData newItem = ScriptableObject.CreateInstance<Item_WeaponData>();
    //    newItem.itemgrade = item4.itemgrade;
    //    newItem.itemType = item4.itemType;
    //    newItem.named = item4.named;
    //    newItem.icon = item4.icon;
    //    Debug.Log("아이템 들어옴");
    //    inventory.Add(item4);
    //}
    //public void Test5(InputAction.CallbackContext _)
    //{
    //    Item_WeaponData newItem = ScriptableObject.CreateInstance<Item_WeaponData>();
    //    newItem.itemgrade = item5.itemgrade;
    //    newItem.itemType = item5.itemType;
    //    newItem.named = item5.named;
    //    newItem.icon = item5.icon;
    //    Debug.Log("아이템 들어옴");
    //    inventory.Add(item5);
    //}
}