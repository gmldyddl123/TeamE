using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SteminaPlus : IncludingStatsActor
{
    /// <summary>
    /// 현재 스테미나
    /// </summary>
    float stamina = 1000.0f;
    public float Stamina
    {
        get => stamina;
        set
        {
            stamina = value;
            if (stamina <= 0)   
            {
                // 달릴 수 없게
            }
            stamina = Mathf.Clamp(stamina, 0, Maxstamina);     // 스테미나는 항상 0~최대치
            onStaminaChange?.Invoke(stamina / Maxstamina);   // 스테미나 변화 알리기
        }
    }

    /// <summary>
    /// 최대 스테미나
    /// </summary>
    float maxstamina = 1000.0f;
    public float Maxstamina => maxstamina;

    /// <summary>
    /// 스테미나가 변경되었을 때 실행될 델리게이트
    /// </summary>
    public Action<float> onStaminaChange { get; set; }
}