using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEquipUI : MonoBehaviour
{
    Button EquipChangeButton;
    Button SkillInfoButton;
    Button ChracterLeverUp;

    TextMeshPro HPText;
    TextMeshPro DefText;
    TextMeshPro AtkText;


    PlayerStat showPlayer;


    public PlayerStat testPlayerStat;

    PlayerStat ShowPlayer
    {
        get => showPlayer;
        set
        {
            if(value != showPlayer)
            {
                showPlayer = value;
                ChangeStatInfoText();
            }
        }
    }


    private void Awake()
    {
        Transform child = transform.GetChild(0).GetChild(0);

        EquipChangeButton = child.GetChild(0).GetComponent<Button>();
        SkillInfoButton = child.GetChild(1).GetComponent<Button>();
        ChracterLeverUp = child.GetChild(2).GetComponent<Button>();


        child = transform.GetChild(0).GetChild(1);

        HPText = child.GetChild(0).GetChild(0).GetComponent<TextMeshPro>();
        DefText = child.GetChild(1).GetChild(0).GetComponent<TextMeshPro>();
        AtkText = child.GetChild(2).GetChild(0).GetComponent<TextMeshPro>();


        ShowPlayer = testPlayerStat;

    }


    void ChangeStatInfoText()
    {
        HPText.text = showPlayer.MaxHP.ToString();
        DefText.text = showPlayer.Def.ToString();
        AtkText.text = showPlayer.Atk.ToString();
    }


}
