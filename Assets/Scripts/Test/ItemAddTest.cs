using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class ItemAddTest : TestBase
{
    PlayerInputAction inputActions;
    public System.Action OnItemDrop;
    private void Awake()
    {
        inputActions = new PlayerInputAction();
    }
    private void OnEnable()
    {
        inputActions.ItemTest.Enable();
        inputActions.ItemTest.Test1.performed += OnTest1;
        inputActions.ItemTest.Test2.performed += OnTest2;
        inputActions.ItemTest.Test3.performed += OnTest3;
        inputActions.ItemTest.Test4.performed += OnTest4;
        inputActions.ItemTest.Test5.performed += OnTest5;
    }
    private void OnDisable()
    {
        inputActions.ItemTest.Disable();
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
        throw new NotImplementedException();
    }

    private void OnTest2(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }


}
