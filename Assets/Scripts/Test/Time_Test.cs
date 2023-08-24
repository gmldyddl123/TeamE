using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using player;

public class Time_Test : TestBase
{
    public PlayerController controller;
    bool isPaused = false;
    protected override void Test3(InputAction.CallbackContext context)
    {
        controller.TestPause();

        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
    }
}