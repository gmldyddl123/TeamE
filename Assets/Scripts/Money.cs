using player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.SpriteAssetUtilities;
using Unity.VisualScripting;
using UnityEngine;

public class Money : MonoBehaviour
{
    PlayerController player;
    TextMeshProUGUI moneyText;

    private void Start()
    {
        // Player ��ũ��Ʈ�� ���� ���� ������Ʈ�� ã�Ƽ� player ������ �Ҵ�
        player = GameManager.Inst.Player;

        moneyText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        // ���÷� "G" Ű�� ������ ��尡 10 �����ϵ��� ����
        if (Input.GetKeyDown(KeyCode.G))
        {
            player.IncreaseGold(10);
        }

        // ���÷� "H" Ű�� ������ ��尡 5 �����ϵ��� ����
        if (Input.GetKeyDown(KeyCode.H))
        {
            player.DecreaseGold(5);
        }
        moneyText.text = player.Gold.ToString();
    }
}