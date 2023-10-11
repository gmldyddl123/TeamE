using player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class StaminaBar : BarBase
{
    public PlayerController playerController;
    private void Start()
    {
        maxValue = playerController.Maxstamina;
        max.text = $"/ {maxValue}";
        current.text = playerController.Stamina.ToString("N0");
        slider.value = playerController.Stamina / maxValue;
        playerController.onStaminaChange += OnValueChange;
    }
}