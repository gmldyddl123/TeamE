using player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class StaminaBar : BarBase
{
    //public PlayerController playerController;
    private void Awake()
    {
        slider = GetComponent<Slider>();
        //Transform child = transform.GetChild(2);
        //current = child.GetComponent<TextMeshProUGUI>();
        //child = transform.GetChild(3);
        //max = child.GetComponent<TextMeshProUGUI>();

        //child = transform.GetChild(0);
        //Image backgroundImage = child.GetComponent<Image>();
        //Color backgroundColor = new Color(color.r, color.g, color.b, color.a * 0.5f);
        //backgroundImage.color = backgroundColor;
        //child = transform.GetChild(1);
        //Image fillImage = child.GetComponentInChildren<Image>();
        //fillImage.color = color;
    }

    private void Start()
    {
        maxValue = playerController.Maxstamina;
        playerController.onStaminaChange += OnValueChange;      
    }

    protected override void OnValueChange(float ratio)
    {     
        slider.value = ratio;
    }
}