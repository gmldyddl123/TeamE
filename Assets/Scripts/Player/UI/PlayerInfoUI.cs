using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour
{

    //���� �������� �ٲ㼭 �������� Ȱ��ȭ ��Ȱ��ȭ �Ѵ�
    GameObject currentPage;
    GameObject CurrentPage
    {
        get => currentPage;
        set
        {
            if (currentPage != value )
            {
                if(currentPage != null)
                {
                    currentPage.SetActive(false);
                }            
                currentPage = value;
                currentPage.SetActive(true);
            }
        }
    }

    //Button LastClickButton;


    //�� �ٲܶ� Ȱ��ȭ �뵵
    GameObject defalutTab;
    GameObject equipTab;
    GameObject skillInfoTab;
    GameObject levelUpTab;

    //�� ����
    Button equipChangeButton;
    Button skillInfoButton;
    Button chracterLeverUp;


    //�⺻�� ���� ǥ�ÿ�
    TextMeshProUGUI hpText;
    TextMeshProUGUI defText;
    TextMeshProUGUI atkText;


    //��� ����������

    Image equipImage;

    TextMeshProUGUI equipName;
    TextMeshProUGUI equipAtk;
    TextMeshProUGUI equipInfo;


    Button equipmentButton;
    Button unequipButton;

    Button equipTabBackToMenu;


    //��ų ���� ������
    Button skillInfoBackToMenu;



    //������ ������

    Button levelUpTabBackToMenu;



    //�׽�Ʈ �뵵 �巡�׾� ������� �÷��̾� ��Ʈ�ѷ��� �ִ� �÷��̾� �Ǹ��� �־��ָ� �ȴ�
    public PlayerStat testPlayerStat;

    //���� ǥ������ ĳ���� �����̸� ���� ��Ʈ�ѷ��� �ִ� ���� ĳ���͸� �ҷ��´�
    PlayerStat showPlayer;


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
        defalutTab = transform.GetChild(0).GetChild(0).gameObject;
        equipTab = transform.GetChild(0).GetChild(1).gameObject;
        skillInfoTab = transform.GetChild(0).GetChild(2).gameObject;
        levelUpTab = transform.GetChild(0).GetChild(3).gameObject;

        Transform child = transform.GetChild(0).GetChild(0).GetChild(0);

        equipChangeButton = child.GetChild(0).GetComponent<Button>();
        skillInfoButton = child.GetChild(1).GetComponent<Button>();
        chracterLeverUp = child.GetChild(2).GetComponent<Button>();
       

        equipChangeButton.onClick.AddListener(() => CurrentPage = equipTab);
        skillInfoButton.onClick.AddListener(() => CurrentPage = skillInfoTab);
        chracterLeverUp.onClick.AddListener(() => CurrentPage = levelUpTab);

        child = gameObject.transform.GetChild(0).GetChild(0).GetChild(1);

        hpText = child.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        defText = child.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        atkText = child.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();


        //��� ���� ��

        child = gameObject.transform.GetChild(0).GetChild(1);



        equipImage = child.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();

        equipName = child.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        equipAtk = child.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        equipInfo = child.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>();


        equipmentButton = child.GetChild(0).GetChild(3).GetComponent<Button>();
        unequipButton = child.GetChild(0).GetChild(4).GetComponent<Button>();


        equipTabBackToMenu = child.GetChild(1).GetComponent<Button>(); ;
        equipTabBackToMenu.onClick.AddListener(() =>
        {
            //LastClickButton = equipTabBackToMenu;
            CurrentPage = defalutTab;
        });

        //��ų������  �� �κ��� �ǵ��� �ʿ䰡 ���� �÷��̾ �ؾ��ϴ� �κ�
        child = gameObject.transform.GetChild(0).GetChild(2);

        skillInfoBackToMenu = child.GetChild(4).GetComponent<Button>();

        skillInfoBackToMenu.onClick.AddListener(() =>
        {
           // LastClickButton = skillInfoBackToMenu;
            CurrentPage = defalutTab;
        });

        currentPage = defalutTab;

        ShowPlayer = testPlayerStat;


        //������ ��

        child = gameObject.transform.GetChild(0).GetChild(3);

        levelUpTabBackToMenu = child.GetChild(0).GetComponent<Button>();

        levelUpTabBackToMenu.onClick.AddListener(() =>
        {
            //LastClickButton = levelUpTabBackToMenu;
            CurrentPage = defalutTab;
        }) ;
    }

    private void OnDisable()
    {
        CurrentPage = defalutTab;
    }


    void ChangeStatInfoText()
    {
        hpText.text = showPlayer.MaxHP.ToString();
        defText.text = showPlayer.Def.ToString();
        atkText.text = showPlayer.Atk.ToString();

    }

}
