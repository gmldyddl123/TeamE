using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace player
{
    public class AttackState : PlayerState
    {
        //���� 0~4
        //������
        //�뽬 ����
        //�ִϸ����� �������̵�
        //�޺� ī����
        int comboCount = 0;
        int maxComboCount = 4;

        float comboTimer = 0.0f;
        float maxComboTimer = 1.5f;

        private bool isAttack = false;

        PlayerInputSystem playerInputSystem;
        State state = State.Attack;

        public AttackState(PlayerInputSystem playerInputSystem)
        {
            this.playerInputSystem = playerInputSystem;
        }

        public void EnterState()
        {
            if(isAttack)
            {
                ComboAttack();
            }
            else
            {
                playerInputSystem.playerCurrentStates = this;
                isAttack = true;
                ComboAttack();
            }

        }
        public void ComboAttack()
        {
            if(comboCount < maxComboCount)
            {
                comboTimer = 0.0f;
                Debug.Log(comboCount++);
            }
        }

        public void MoveLogic()
        {
            Debug.Log(isAttack);
            comboTimer += Time.deltaTime;
            //Debug.Log(comboTimer);
            if (comboTimer > maxComboTimer)
            {
                comboCount = 0;
                comboTimer = 0.0f;
                isAttack = false;
                playerInputSystem.PlayerEnterIdleState();
                
            }

            //������ ��¦ ����
        }
    }
}

