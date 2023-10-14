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

        playerName = "¿©ÇàÀÚ";    
        //skillEffect
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
