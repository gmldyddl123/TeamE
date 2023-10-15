using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillBar : BarBase
{
    TextMeshProUGUI text;
    //TextMeshProUGUI max;

    public Color color = Color.white;



    private void Awake()
    {
        slider = GetComponent<Slider>();
        Transform child = transform.GetChild(2);
        text = child.GetComponent<TextMeshProUGUI>();
        //child = transform.GetChild(3);
        //max = child.GetComponent<TextMeshProUGUI>();

        child = transform.GetChild(0);
        Image backgroundImage = child.GetComponent<Image>();
        Color backgroundColor = new Color(color.r, color.g, color.b, color.a * 0.5f);
        backgroundImage.color = backgroundColor;

        child = transform.GetChild(1);
        Image fillImage = child.GetComponentInChildren<Image>();
        fillImage.color = color;

        playerController.characterChangeUIBar += ChangeCharacter;
    }


    protected override void OnValueChange(float skillGauge)
    {
        //ratio = Mathf.Clamp01(ratio);               // ratio를 0~1로 변경
        float ratio = skillGauge / maxValue;
        slider.value = ratio;                       // 슬라이더 조정


        if(skillGauge >= maxValue)
        {
            text.text = $"MAX";  // 글자 변경
        }
        else
        {
            text.text = $"";  // 글자 변경
        }



    }

    public void PublicOnValueChange(float skillGauge)
    {
        OnValueChange(skillGauge);
    }

    public void ChangeCharacter(PlayerStat playerCurrentPlayer)
    {
        maxValue = playerCurrentPlayer.MaxSkillGauge;
        OnValueChange(playerCurrentPlayer.CurrentSkillGauge);
        //playerCurrentPlayer.onHealthChange += OnValueChange;
    }
}
