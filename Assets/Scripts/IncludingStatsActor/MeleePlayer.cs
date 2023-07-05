using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class MeleePlayer : PlayerStat
{
    float attackForwardMoveSpeed = 5.0f;

    protected override void Update()
    {
        base.Update();
    }

    public override void Attack(Vector3 movedir)
    {

         //characterController.Move(attackForwardMoveSpeed * Time.fixedDeltaTime * movedir);
        //�ٰŸ��� ������
        //���Ÿ��� �ڷ�
        //playerInputSystem.UseGravity();
        //������ ��¦ ���� attackMove ���� �ִϸ��̼� �̹�Ʈ���� ����ȴ�
        //base.Attack(movedir);
        if (playerInputSystem.attackMove)
        {
            //playerInputSystem.UseGravity();
            characterController.Move(attackForwardMoveSpeed * Time.fixedDeltaTime * movedir);
        }
    }
}
