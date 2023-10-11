using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : BarBase
{
    public PlayerController playerController;
    private void Start()
    {
        playerController.characterChangeHpBar += ChangeCharacter;
        ////maxValue = state.MaxHP;
        //max.text = $"/ {maxValue}";
        //current.text = playerCurrentPlayer.HP.ToString("N0");
        //slider.value = playerCurrentPlayer.HP / maxValue;
        //playerCurrentPlayer.onHealthChange += OnValueChange;
    }


    public void ChangeCharacter(PlayerStat playerCurrentPlayer)
    {
        maxValue = playerCurrentPlayer.HP;
        max.text = $"/ {maxValue}";
        current.text = playerCurrentPlayer.HP.ToString("N0");
        slider.value = playerCurrentPlayer.HP / maxValue;
        //playerCurrentPlayer.onHealthChange += OnValueChange;
    }
}
