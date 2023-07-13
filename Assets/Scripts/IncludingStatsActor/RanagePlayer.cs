using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RanagePlayer : PlayerStat
{
    private void Awake()
    {
        attackMoveSpeed = -2.0f;
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Attack(Vector3 movedir)
    {

        base.Attack(movedir);
    }
}
