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
        skillColliderComponent.atkPower = EnemyTargetDamage;

        maxHP = 50.0f;
        Atk = 20.0f;
        Def = 15.0f;

        playerName = "여행자";

        CalculatedAttackPower = Atk;

        //스킬 카메라

        skillCart = transform.GetChild(3).GetChild(0).GetComponent<CinemachineDollyCart>();
        skillCutSceneCamera = transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<CinemachineVirtualCamera>();


        //콤보 데미지
        
        attackDamageCalculation[0] = 1.25f; //콤보1

        attackDamageCalculation[1] = 1.2f;  //콤보2

        attackDamageCalculation[2] = 1.4f;  //콤보3

        attackDamageCalculation[3] = 1.25f; //콤보4의 1타 2타
        attackDamageCalculation[4] = 1.6f;


        attackDamageCalculation[maxAttackCount-1] = 2.85f; // 스킬
    }

    public override void SkillCameraOn()
    {
        skillCart.m_Speed = 4.0f;
        skillCutSceneCamera.Priority = 50;
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
