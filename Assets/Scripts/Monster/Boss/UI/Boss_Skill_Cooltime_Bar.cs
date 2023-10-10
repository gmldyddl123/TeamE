using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Skill_Cooltime_Bar : Boss_BarBase
{
    private float skillCooldownRatio;
    
   
    void Update()
    {
        skillCooldownRatio = 1.0f / boss.skillCoolTime;

        float result = boss.skillCooldownTime * skillCooldownRatio;

        slider.value = result;
    }
}
