
using boss;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Boss_BarBase : MonoBehaviour
{
    protected Slider slider;
    protected float maxValue;
    protected Boss_Monster boss;

    protected void Awake()
    {
        slider = GetComponent<Slider>();
        boss = FindObjectOfType<Boss_Monster>();
    }

    protected void ValueChange(float ratio) 
    {
        ratio = Mathf.Clamp01(ratio);               
        slider.value = ratio;                       
    }
}

