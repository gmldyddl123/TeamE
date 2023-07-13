using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class MeleePlayer : PlayerStat
{

    private void Awake()
    {
        attackMoveSpeed = 3.0f;
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Attack(Vector3 movedir)
    {

        //base.Attack(movedir);
        //playerInputSystem.UseGravity();
        //적한테 살짝 접근 attackMove 값은 애니메이션 이밴트에서 실행된다
        //아래로 이동하는 용도임 없으면 좀 꼬임 공통된 행동이라 어택 스테이트로 옮기는게 좋을거 같아


        characterController.Move(
             Vector3.down * 3.0f
             * Time.fixedDeltaTime);
        if (playerInputSystem.attackMove)
        {
            //playerInputSystem.UseGravity();
            characterController.Move(attackMoveSpeed * Time.fixedDeltaTime * movedir);
        }

    }
}
