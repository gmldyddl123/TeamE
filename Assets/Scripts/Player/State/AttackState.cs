using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace player
{
    public class AttackState : PlayerState
    {
        //어택 0~4 o
        //강공격
        //대쉬 공격
        //애니메이터 오버라이딩 o
        //콤보 카운터 o
        int comboCount = 0;
        int maxComboCount = 4;

        float comboTimer = 0.0f;
        float maxComboTimer = 1.5f;

        //private bool isAttack = false;

        //Transform target;
        Vector3 moveTargetDir;
        //float attackForwardMoveSpeed = 5.0f;


        PlayerInputSystem playerInputSystem;
        //CharacterController characterController;
        Animator animator;
        State state = State.Attack;

        public Action<Vector3> attackMove;

        public AttackState(PlayerInputSystem playerInputSystem, Animator animator)
        {
            this.playerInputSystem = playerInputSystem;
            this.animator = animator;
            //this.characterController = characterController;
        }

        public void EnterState()
        {
            if(playerInputSystem.playerCurrentStates is AttackState)
            {
                ComboAttack();
                //attack?.Invoke();
            }
            else
            {
                playerInputSystem.playerCurrentStates = this;
                playerInputSystem.isAttack = true;
                playerInputSystem.PlayerAnimoatrChage((int)state);
                ComboAttack();
                //attack?.Invoke();
            }

        }
        public void ComboAttack()
        {
            if(comboCount < maxComboCount)
            {
                SummonWeapon(true);
                playerInputSystem.MoveToDir();
                moveTargetDir = playerInputSystem.moveDirection;
                //attack?.Invoke();
                comboTimer = 0.0f;
                animator.SetInteger("ComboCount", comboCount++);
            }
        }

        public void MoveLogic()
        {
            comboTimer += Time.deltaTime;
            //playerInputSystem.TestGravity();
            ////적한테 살짝 접근 attackMove 값은 애니메이션 이밴트에서 실행된다
            //if (playerInputSystem.attackMove)
            //{
            //    //playerInputSystem.UseGravity();
            //    characterController.Move(attackForwardMoveSpeed * Time.fixedDeltaTime * moveTargetDir);
            //}

            attackMove?.Invoke(moveTargetDir);
            //playerInputSystem.UseGravity();
            
            
            //애니메이션 다 재생되면 소환해제
            if (comboTimer > maxComboTimer)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    SummonWeapon(false);
                    playerInputSystem.PlayerEnterIdleState();
                }
                ResetCombo();
            }



            
        }

        public void ExitAttackState()
        {
            ResetCombo();
            SummonWeapon(false);
        }

        private void ResetCombo()
        {
            comboCount = 0;
            comboTimer = 0.0f;
            playerInputSystem.isAttack = false;
        }
        private void SummonWeapon(bool summon)
        {
            playerInputSystem.handWeapon.SetActive(summon);
            playerInputSystem.backWeapon.SetActive(!summon);
        }
    }
}

