using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatsActor : IncludingStatsActor
{
    public override void OnDamage(float damage)
    {
        HP -= damage;
    }

}
