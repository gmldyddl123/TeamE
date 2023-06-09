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
        //근거리는 앞으로
        //원거리는 뒤로
        //playerInputSystem.UseGravity();
        //적한테 살짝 접근 attackMove 값은 애니메이션 이밴트에서 실행된다
        //base.Attack(movedir);
        if (playerInputSystem.attackMove)
        {
            //playerInputSystem.UseGravity();
            characterController.Move(attackForwardMoveSpeed * Time.fixedDeltaTime * movedir);
        }
    }
}
