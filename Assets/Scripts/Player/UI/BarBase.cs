using player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarBase : MonoBehaviour
{
    public PlayerController playerController;

    protected float maxValue;

    protected Slider slider;

    /// <summary>
    /// 바가 표시할 값이 변경되었을 때 실행될 함수
    /// </summary>
    /// <param name="ratio">변경된 비율</param>
    protected virtual void OnValueChange(float ratio)
    {

    }


}
