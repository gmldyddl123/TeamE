using player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour
{

    public GameObject playerCamera;
    public GameObject uiCamera;


    public GameObject uiCanvas;
    public GameObject equipCanvas;


    //현재 페이지를 바꿔서 페이지를 활성화 비활성화 한다
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


    //최상단 캐릭터 탭

    Transform topTabButtonTransform;

    PlayerController playerController;

    public GameObject charaterButtonPrefab;


    //테스트 용도 드래그앤 드롭으로 플레이어 컨트롤러에 있는 플레이어 피메일 넣어주면 된다
    //public PlayerStat testPlayerStat;

    //현재 표시중인 캐릭터 유아이를 열때 컨트롤러에 있는 현재 캐릭터를 불러온다


    PlayerStat LastPlayPlayer;

    PlayerStat showPlayer = null;


    public PlayerStat ShowPlayer
    {
        get => showPlayer;
        set
        {
            if (value != showPlayer)
            {
                if(showPlayer != null)
                {
                    showPlayer.gameObject.SetActive(false);
                }
                showPlayer = value;
                showPlayer.gameObject.SetActive(true);
                ChangeStatInfoText();
            }
        }
    }


    //탭 바꿀때 활성화 용도
    GameObject defalutTab;
    GameObject equipTab;
    GameObject skillInfoTab;
    GameObject levelUpTab;

    //탭 변경
    Button equipChangeButton;
    Button skillInfoButton;
    Button chracterLeverUp;


    //기본탭 스탯 표시용
    TextMeshProUGUI hpText;
    TextMeshProUGUI defText;
    TextMeshProUGUI atkText;


    //장비 장착페이지

    Image equipImage;

    TextMeshProUGUI equipName;
    TextMeshProUGUI equipAtk;
    TextMeshProUGUI equipInfo;


    Button equipmentButton;
    Button unequipButton;

    Button equipTabBackToMenu;


    //스킬 인포 페이지
    Button skillInfoBackToMenu;



    //레벨업 페이지

    Button levelUpTabBackToMenu;



    


    private void Awake()
    {

        //최상단 탭
        topTabButtonTransform = transform.GetChild(0).GetChild(4).GetChild(0).GetChild(0).GetChild(0).gameObject.transform;


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


        //장비 장착 탭

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

        //스킬인포탭  이 부분은 건들일 필요가 없다 플레이어가 해야하는 부분
        child = gameObject.transform.GetChild(0).GetChild(2);

        skillInfoBackToMenu = child.GetChild(4).GetComponent<Button>();

        skillInfoBackToMenu.onClick.AddListener(() =>
        {
           // LastClickButton = skillInfoBackToMenu;
            CurrentPage = defalutTab;
        });

        currentPage = defalutTab;


        //레벨업 탭

        child = gameObject.transform.GetChild(0).GetChild(3);

        levelUpTabBackToMenu = child.GetChild(0).GetComponent<Button>();

        levelUpTabBackToMenu.onClick.AddListener(() =>
        {
            //LastClickButton = levelUpTabBackToMenu;
            CurrentPage = defalutTab;
        }) ;



        playerController = FindAnyObjectByType<PlayerController>();
        TopCharacterButtonSetting(playerController.pickChr);


    }
    private void OnEnable()
    {
        Debug.Log(ShowPlayer);
        playerController.StopInputKey(false);

        equipCanvas.SetActive(true);
        uiCanvas.SetActive(false);

        playerCamera.SetActive(false);
        uiCamera.SetActive(true);


        LastPlayPlayer = playerController.currentPlayerCharacter;
        ShowPlayer = LastPlayPlayer;
    }

    private void OnDisable()
    {
        
        ShowPlayer = LastPlayPlayer;
        CurrentPage = defalutTab;
        uiCamera.SetActive(false);
        playerCamera.SetActive(true);

        uiCanvas.SetActive(false);
        equipCanvas.SetActive(true);


        playerController.StopInputKey(true);
    }


    void ChangeStatInfoText()
    {
        hpText.text = showPlayer.MaxHP.ToString();
        defText.text = showPlayer.Def.ToString();
        atkText.text = showPlayer.Atk.ToString();

    }    


    public void TopCharacterButtonSetting(PlayerStat[] character)
    {
        for(int i = 0; i < character.Length; i++)
        {
            GameObject topButton = Instantiate(charaterButtonPrefab, topTabButtonTransform);
            topButton.GetComponent<CharacterInfoTopButton>().InitButton(character[i], this);
            
        }
    }
}
