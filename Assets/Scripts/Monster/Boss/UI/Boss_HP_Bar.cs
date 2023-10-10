using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_HP_Bar : Boss_BarBase
{
    void Start()
    {
        boss.bossHealthChange += ValueChange;
    }

}
