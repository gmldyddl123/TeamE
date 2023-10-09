using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class StaminaBar : BarBase
{
    public SteminaPlus state;
    private void Start()
    {
        maxValue = state.Maxstamina;
        max.text = $"/ {maxValue}";
        current.text = state.Stamina.ToString("N0");
        slider.value = state.Stamina / maxValue;
        state.onStaminaChange += OnValueChange;
    }
}