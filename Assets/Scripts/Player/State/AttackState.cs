using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace player
{
    public class AttackState : MonoBehaviour, PlayerState
    {
        //���� 0~4
        //������
        //�뽬 ����
        //�ִϸ����� �������̵�
        //�޺� ī����
        public int comboCount = 0;

        PlayerInputSystem playerInputSystem;
        State state = State.Attack;

        public AttackState(PlayerInputSystem playerInputSystem)
        {
            this.playerInputSystem = playerInputSystem;
        }
        private void Update()
        {
            //������ Ÿ�̸�, �޺����� Ÿ�̸�
        }

        public void EnterState()
        {
            playerInputSystem.playerCurrentStates = this;
        }

        public void MoveLogic()
        {

        }
    }
}

