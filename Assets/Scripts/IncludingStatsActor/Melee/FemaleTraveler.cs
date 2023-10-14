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
        base.Awake();
        Debug.Log(onHealthChange);
        skillCollider = transform.GetChild(0).GetChild(0).GetComponent<SphereCollider>();
        skillEffect = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<ParticleSystem>();
        AttackCollider skillColliderComponent = skillCollider.GetComponent<AttackCollider>();
        skillColliderComponent.atkPower = EnemyTargetDamage;

        maxHP = 50.0f;
        Atk = 20.0f;
        Def = 15.0f;

        playerName = "여행자";



        //스킬 카메라

        skillCart = transform.GetChild(3).GetChild(0).GetComponent<CinemachineDollyCart>();
        skillCutSceneCamera = transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<CinemachineVirtualCamera>();


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
