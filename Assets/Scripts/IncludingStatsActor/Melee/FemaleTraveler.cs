using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FemaleTraveler : MeleePlayer
{
    GameObject skillEffect;

    

    private void Awake()
    {
        skillEffect = transform.GetChild(0).GetChild(0).gameObject;
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


    public void SkillEffectOnOff()
    {
        skillEffect.SetActive(skillEffect.activeSelf ? false : true); 
    }

    public void ExitSkillState()
    {
        playerController.PlayerEnterIdleState();
    }
}
