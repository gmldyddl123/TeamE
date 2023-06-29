using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : IncludingStatsActor
{
    public CapsuleCollider attackCollider;
    protected override void Attack()
    {
        //근거리는 앞으로
        //원거리는 뒤로
    }

    public void AttackColliderActive()
    {
        attackCollider.enabled = attackCollider.enabled ? attackCollider.enabled : false;
    }
}
