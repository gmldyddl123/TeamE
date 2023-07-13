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


        //�Ʒ��� �̵��ϴ� �뵵�� ������ �� ���� ����� �ൿ�̶� ���� ������Ʈ�� �ű�°� ������ ����
        characterController.Move(
             Vector3.down * 3.0f
             * Time.fixedDeltaTime);
        if (playerInputSystem.attackMove)
        {
            //playerInputSystem.UseGravity();
            characterController.Move(attackForwardMoveSpeed * Time.fixedDeltaTime * movedir);
        }
        //else
        //{
        //    characterController.Move(
        //      Vector3.down * 3.0f
        //      * Time.fixedDeltaTime);

        //}
    }
}
