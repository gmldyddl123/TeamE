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

    public override void AttackMove(Vector3 movedir)
    {

        //base.Attack(movedir);
        //playerInputSystem.UseGravity();
        //������ ��¦ ���� attackMove ���� �ִϸ��̼� �̹�Ʈ���� ����ȴ�
        //�Ʒ��� �̵��ϴ� �뵵�� ������ �� ���� ����� �ൿ�̶� ���� ������Ʈ�� �ű�°� ������ ����


        characterController.Move(
             Vector3.down * 3.0f
             * Time.fixedDeltaTime);
        if (attackMove)
        {
            //playerInputSystem.UseGravity();
            characterController.Move(attackMoveSpeed * Time.fixedDeltaTime * movedir);
        }

    }

}
