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

    public AnimatorOverrideController animator;



    private void Awake()
    {
        //GetComponentInParent<PlayerInputSystem>();
        playerInputSystem = GetComponentInParent<PlayerInputSystem>();
        characterController = GetComponentInParent<CharacterController>();
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
