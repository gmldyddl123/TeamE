using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : IncludingStatsActor
{
    public CapsuleCollider attackCollider;
    protected override void Attack()
    {
        //�ٰŸ��� ������
        //���Ÿ��� �ڷ�
    }

    public void AttackColliderActive()
    {
        attackCollider.enabled = attackCollider.enabled ? attackCollider.enabled : false;
    }
}
