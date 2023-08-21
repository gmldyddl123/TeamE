using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using player;

public class Test : TestBase
{
    public PlayerController controller;
    
    protected override void Test1(InputAction.CallbackContext context)
    {
        controller.TestStop();
        
        Time.timeScale = 0.0f;
    }
}
