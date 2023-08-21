using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HP_Test : TestBase
{
    public IncludingStatsActor state;
    protected override void Test1(InputAction.CallbackContext context)
    {
        state.HP -= 30f;
    }
    protected override void Test2(InputAction.CallbackContext context)
    {
        state.HP += 30f;
    }
}
