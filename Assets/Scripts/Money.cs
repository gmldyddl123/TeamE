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
        // Player 스크립트를 가진 게임 오브젝트를 찾아서 player 변수에 할당
        player = GameManager.Inst.Player;

        moneyText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        // 예시로 "G" 키를 누르면 골드가 10 증가하도록 설정
        if (Input.GetKeyDown(KeyCode.G))
        {
            player.IncreaseGold(10);
        }

        // 예시로 "H" 키를 누르면 골드가 5 감소하도록 설정
        if (Input.GetKeyDown(KeyCode.H))
        {
            player.DecreaseGold(5);
        }
        moneyText.text = player.Gold.ToString();
    }
}