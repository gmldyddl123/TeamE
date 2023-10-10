using boss;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Groggy_Bar : Boss_BarBase
{

    void Start()
    {
        boss.bossGroggyChange += ValueChange;
    }
}

