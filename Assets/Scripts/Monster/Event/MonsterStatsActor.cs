using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatsActor : IncludingStatsActor
{
    public override void OnDamage(int damage, float criChance)
    {
        HP -= damage;
    }

}
