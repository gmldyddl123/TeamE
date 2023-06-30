using player;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerStat : IncludingStatsActor
{
    public PlayerInputSystem playerInputSystem;
    public CharacterController characterController;
    public CapsuleCollider attackCollider;

    //Vector3 moveTargetDir;
    float attackForwardMoveSpeed = 5.0f;




    private void Awake()
    {
        playerInputSystem = GetComponent<PlayerInputSystem>();
        characterController = GetComponent<CharacterController>();
    }



    public override void Attack(Vector3 movedir)
    {
        //근거리는 앞으로
        //원거리는 뒤로

        Debug.Log("델리게이트 어택");

        playerInputSystem.UseGravity();
        //적한테 살짝 접근 attackMove 값은 애니메이션 이밴트에서 실행된다
        if (playerInputSystem.attackMove)
        {
            //playerInputSystem.UseGravity();
            characterController.Move(attackForwardMoveSpeed * Time.fixedDeltaTime * movedir);
        }
    }


    public void AttackColliderActive()
    {

        attackCollider.enabled = attackCollider.enabled ? false : true;

    }

    public void AttackColliderDisable()
    {
        attackCollider.enabled = false;
    }
}
