using player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : BarBase
{

    TextMeshProUGUI current;
    TextMeshProUGUI max;

    public Color color = Color.white;

    

    private void Awake()
    {
        slider = GetComponent<Slider>();
        Transform child = transform.GetChild(2);
        current = child.GetComponent<TextMeshProUGUI>();
        child = transform.GetChild(3);
        max = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(0);
        Image backgroundImage = child.GetComponent<Image>();
        Color backgroundColor = new Color(color.r, color.g, color.b, color.a * 0.5f);
        backgroundImage.color = backgroundColor;

        child = transform.GetChild(1);
        Image fillImage = child.GetComponentInChildren<Image>();
        fillImage.color = color;

        playerController.characterChangeUIBar += ChangeCharacter;
    }


    protected override void OnValueChange(float hp)
    {
        //ratio = Mathf.Clamp01(ratio);               // ratio를 0~1로 변경
        float ratio = hp / maxValue;
        slider.value = ratio;                       // 슬라이더 조정
        current.text = $"{hp:f0}";  // 글자 변경



    }

    public void PublicOnValueChange(float hp)
    {
        OnValueChange(hp);
    }

    public void ChangeCharacter(PlayerStat playerCurrentPlayer)
    {
        maxValue = playerCurrentPlayer.MaxHP;
        max.text = $"/ {maxValue}";
        current.text = playerCurrentPlayer.HP.ToString("N0");
        slider.value = playerCurrentPlayer.HP / maxValue;
        //playerCurrentPlayer.onHealthChange += OnValueChange;
    }
}
