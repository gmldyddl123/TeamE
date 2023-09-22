using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SteminaPlus : IncludingStatsActor
{
    /// <summary>
    /// ���� ���׹̳�
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
                // �޸� �� ����
            }
            stamina = Mathf.Clamp(stamina, 0, Maxstamina);     // ���׹̳��� �׻� 0~�ִ�ġ
            onStaminaChange?.Invoke(stamina / Maxstamina);   // ���׹̳� ��ȭ �˸���
        }
    }

    /// <summary>
    /// �ִ� ���׹̳�
    /// </summary>
    float maxstamina = 1000.0f;
    public float Maxstamina => maxstamina;

    /// <summary>
    /// ���׹̳��� ����Ǿ��� �� ����� ��������Ʈ
    /// </summary>
    public Action<float> onStaminaChange { get; set; }
}