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
        //�ٰŸ��� ������
        //���Ÿ��� �ڷ�

        Debug.Log("��������Ʈ ����");

        playerInputSystem.UseGravity();
        //������ ��¦ ���� attackMove ���� �ִϸ��̼� �̹�Ʈ���� ����ȴ�
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
