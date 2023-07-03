using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePlayer : PlayerStat
{
    float attackForwardMoveSpeed = 5.0f;
    public override void Attack(Vector3 movedir)
    {
        //�ٰŸ��� ������
        //���Ÿ��� �ڷ�
        playerInputSystem.UseGravity();
        //������ ��¦ ���� attackMove ���� �ִϸ��̼� �̹�Ʈ���� ����ȴ�
        if (playerInputSystem.attackMove)
        {
            //playerInputSystem.UseGravity();
            characterController.Move(attackForwardMoveSpeed * Time.fixedDeltaTime * movedir);
        }
    }
}
