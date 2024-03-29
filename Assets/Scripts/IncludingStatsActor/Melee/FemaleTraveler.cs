using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FemaleTraveler : MeleePlayer
{
    //Collider skillCollider;
    //protected ParticleSystem skillEffect;



    protected override void Awake()
    {
        maxAttackCount = 6;

        base.Awake();

        skillCollider = transform.GetChild(0).GetChild(0).GetComponent<SphereCollider>();
        skillEffect = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<ParticleSystem>();
        AttackCollider skillColliderComponent = skillCollider.GetComponent<AttackCollider>();
        Debug.Log(skillColliderComponent);
        skillColliderComponent.atkPower += EnemyTargetDamage;
        //skillColliderComponent.skillGaugeUp += SkillGaugeUp; //�����ڵ� ����

        //Debug.Log(skillColliderComponent.skillGaugeUp);

        maxHP = 900.0f;
        Atk = 20.0f;
        Def = 15.0f;

        playerName = "������";

        CalculatedAttackPower = Atk;

        CurrentSkillGauge = maxSkillGauge;

        //��ų ī�޶�

        skillCart = transform.GetChild(3).GetChild(0).GetComponent<CinemachineDollyCart>();
        skillCutSceneCamera = transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<CinemachineVirtualCamera>();


        //�޺� ������
        
        attackDamageCalculation[0] = 1.25f; //�޺�1

        attackDamageCalculation[1] = 1.2f;  //�޺�2

        attackDamageCalculation[2] = 1.4f;  //�޺�3

        attackDamageCalculation[3] = 1.25f; //�޺�4�� 1Ÿ 2Ÿ
        attackDamageCalculation[4] = 1.6f;


        attackDamageCalculation[maxAttackCount-1] = 2.85f; // ��ų
    }

    public override void SkillCameraOn()
    {
        base.SkillCameraOn();
        skillCart.m_Speed = 4.0f;
        //skillCutSceneCamera.Priority = 50;
    }

    public override void UltimateSkill()
    {
        
        characterController.Move(
            Vector3.down * 3.0f
            * Time.fixedDeltaTime);

        if (attackMove)
        {
            characterController.Move(attackMoveSpeed * Time.fixedDeltaTime * playerController.moveDirection);
            
        }
    }

}
