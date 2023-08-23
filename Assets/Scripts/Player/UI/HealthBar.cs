using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : BarBase
{
    public IncludingStatsActor state;
    private void Start()
    {
        //maxValue = state.MaxHP;
        max.text = $"/ {maxValue}";
        current.text = state.HP.ToString("N0");
        slider.value = state.HP / maxValue;
       // state.onHealthChange += OnValueChange;
    }
}
