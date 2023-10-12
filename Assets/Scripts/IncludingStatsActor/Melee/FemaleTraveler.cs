using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FemaleTraveler : MeleePlayer
{
    SphereCollider skillCollider;
    ParticleSystem skillEffect;

    

    protected override void Awake()
    {
        base.Awake();
        Debug.Log(onHealthChange);
        this.skillCollider = transform.GetChild(0).GetChild(0).GetComponent<SphereCollider>();
        skillEffect = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<ParticleSystem>();
        MelleAttackCollider skillColliderComponent = skillCollider.GetComponent<MelleAttackCollider>();
        skillColliderComponent.atkPower = EnemyTargetDamage;
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


    public void SkillColliderActive()
    {
        comboCount = 10;
        skillCollider.enabled = (skillCollider.enabled ? false : true);

    }

    public void SkillEffectOn()
    {
        //skillEffect.SetActive(skillEffect.activeSelf ? false : true); 
        skillEffect.Play();
    }

    public void ExitSkillState()
    {
        comboCount = 0;
        playerController.PlayerEnterIdleState();
    }
}
