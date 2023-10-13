using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoTopButton : MonoBehaviour
{
    PlayerStat player;
    Image portrait;
    Button button;

    private void Awake()
    {
        portrait = GetComponent<Image>();
        button = gameObject.GetComponent<Button>();
    }


    public void InitButton(PlayerStat playerStat , PlayerInfoUI playerInfoUI)
    {
        this.player = playerStat;
        portrait.sprite = playerStat.portrait;
        button.onClick.AddListener(() => playerInfoUI.ShowPlayer = player);
       
    }

}
