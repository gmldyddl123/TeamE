using JetBrains.Annotations;
using player;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class MeleePlayer : PlayerStat
{
    public CapsuleCollider attackCollider;

    

    protected override void Awake()
    {
        base.Awake();
        attackMoveSpeed = 3.0f;
        //attackCollider.gameObject.GetComponent<MelleAttackCollider>().atkPower = ATK;
        attackCollider.enabled = false;

        //�⺻����
        //MelleAttackCollider y = attackCollider.gameObject.GetComponent<MelleAttackCollider>();
        attackCollider.gameObject.GetComponent<MelleAttackCollider>().atkPower = EnemyTargetDamage;


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

    public void AttackColliderActive()
    {
        if (!attackCollider.enabled)
        {
            comboCount++;
        }
        attackCollider.enabled = attackCollider.enabled ? false : true;
        
    }

    public void Test()
    {
        Debug.Log("yest");
    }

    public void AttackColliderDisable()
    {
        attackCollider.enabled = false;
    }

}
