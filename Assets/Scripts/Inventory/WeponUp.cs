using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.Arm;

public class WeponUp : MonoBehaviour
{
    public TextMeshProUGUI oneOreCountText;
    public TextMeshProUGUI twoOreCountText;
    public TextMeshProUGUI threeOreCountText;
    public TextMeshProUGUI oneOreSelectText;
    public TextMeshProUGUI twoOreSelectText;
    public TextMeshProUGUI threeOreSelectText;
    TextMeshProUGUI WeponPower;
    TextMeshProUGUI WeponUpPower;
    Button oneOreButton;
    Button twoOreButton;
    Button threeOreButton;
    Button upButton;
    Slider oneOreSlider;
    Slider twoOreSlider;
    Slider threeOreSlider;
    Slider expSlider;
    public Action OnSelectOre;
    private int selectedOneOre;
    public int SelectedOneOre
    {
        get { return selectedOneOre; }
        set
        {
            selectedOneOre = Mathf.Clamp(value, 0, (int)oneOreSlider.maxValue);
            oneOreSelectText.text = selectedOneOre.ToString();
        }
    }
    int selectedTwoOre = 0;
    public int SelectedTwoOre
    {
        get { return selectedTwoOre; }
        set
        {
            selectedTwoOre = Mathf.Clamp(value, 0, (int)twoOreSlider.maxValue);
            twoOreSelectText.text = selectedTwoOre.ToString();
        }
    }
    int selectedThreeOre = 0;
    public int SelectedThreeOre
    {
        get { return selectedThreeOre; }
        set
        {
            selectedThreeOre = Mathf.Clamp(value, 0, (int)threeOreSlider.maxValue);
            threeOreSelectText.text = selectedThreeOre.ToString();
        }
    }
    public Item_WeaponData wepon;
    private void Awake()
    {
        GetChildComponents();
    }

    void Start()
    {
        AssignButtonListeners();
        InitializeSliders();
    }

    private void OnEnable()
    {
        ResetSelectedOres();
        OreCountSet();
    }
    private void GetChildComponents()
    {
        WeponPower = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        WeponUpPower = transform.GetChild(5).GetComponent<TextMeshProUGUI>();
        oneOreButton = transform.GetChild(7).GetComponent<Button>();
        twoOreButton = transform.GetChild(8).GetComponent<Button>();
        threeOreButton = transform.GetChild(9).GetComponent<Button>();
        oneOreSlider = transform.GetChild(10).GetComponent<Slider>();
        twoOreSlider = transform.GetChild(11).GetComponent<Slider>();
        threeOreSlider = transform.GetChild(12).GetComponent<Slider>();
        upButton = transform.GetChild(13).GetComponent<Button>();
        expSlider = transform.GetChild(15).GetComponent<Slider>();
    }

    private void AssignButtonListeners()
    {
        oneOreButton.onClick.AddListener(() => IncrementOre(ref selectedOneOre, oneOreSlider, oneOre));
        twoOreButton.onClick.AddListener(() => IncrementOre(ref selectedTwoOre, twoOreSlider, twoOre));
        threeOreButton.onClick.AddListener(() => IncrementOre(ref selectedThreeOre, threeOreSlider, threeore));
        upButton.onClick.AddListener(WeponUpgrade);

        oneOreSlider.onValueChanged.AddListener(UpdateOneOre);
        twoOreSlider.onValueChanged.AddListener(UpdateTwoOre);
        threeOreSlider.onValueChanged.AddListener(UpdateThreeOre);
    }

    private void InitializeSliders()
    {
        expSlider.maxValue = 100;
        expSlider.value = wepon.exp;
    }

    private void ResetSelectedOres()
    {
        selectedOneOre = 0;
        selectedTwoOre = 0;
        selectedThreeOre = 0;

        oneOreSelectText.text = selectedOneOre.ToString();
        twoOreSelectText.text = selectedTwoOre.ToString();
        threeOreSelectText.text = selectedThreeOre.ToString();
    }
    void WeponUpgrade()
    {
        wepon.level = Viewlevel;
        wepon.exp = ViewEXP;
        wepon.plusAttack = ViewAttack;
        oneoreCount -= selectedOneOre;
        twooreCount -= selectedTwoOre;
        threeoreCount -= selectedThreeOre;

        // 슬라이더 업데이트
        oneOreSlider.maxValue = oneoreCount;
        twoOreSlider.maxValue = twooreCount;
        threeOreSlider.maxValue = threeoreCount;

        oneOreSlider.value -= selectedOneOre; 
        twoOreSlider.value -= selectedTwoOre;
        threeOreSlider.value -= selectedThreeOre;

        // 선택된 ore의 수를 0으로 리셋
        selectedOneOre = 0;
        selectedTwoOre = 0;
        selectedThreeOre = 0;

        oneOreCountText.text = oneoreCount.ToString();
        twoOreCountText.text = twooreCount.ToString();
        threeOreCountText.text = threeoreCount.ToString();

        oneOreSelectText.text = selectedOneOre.ToString();
        twoOreSelectText.text = selectedTwoOre.ToString();
        threeOreSelectText.text = selectedThreeOre.ToString();

        expSlider.value = wepon.exp;
        Inventory.instance.RemoveOre(10, selectedOneOre);
        Inventory.instance.RemoveOre(11, selectedTwoOre);
        Inventory.instance.RemoveOre(12, selectedThreeOre);
        UpdateWeponUp();
        WeponPower.text = wepon.plusAttack.ToString("F2");
        WeponUpPower.text = (wepon.plusAttack + 10).ToString();
        ViewAttack = wepon.plusAttack;
        Viewlevel = wepon.level;
        ViewEXP = wepon.exp;

    }

    float ViewAttack;
    int Viewlevel;
    float ViewEXP;
    public void WeponGet(ItemData weponItem)
    {
        if (weponItem is Item_WeaponData weaponData)
        {
            wepon = weaponData;
            ViewAttack = wepon.plusAttack;
            Viewlevel = wepon.level;
            ViewEXP = wepon.exp;
            UpdateWeponUp();
        }
    }
   
    private void UpdateWeponUp()
    {

        WeponPower.text = ViewAttack.ToString("F2");
        WeponUpPower.text = (ViewAttack + 10).ToString();
    }
    int oneoreCount;
    int twooreCount;
    int threeoreCount;
    void OreCountSet()
    {
        Inventory inventory = Inventory.instance;
        oneoreCount = inventory.GetOneOreCount();
        oneOreCountText.text = oneoreCount.ToString();
        twooreCount = inventory.GetTwoOreCount();
        twoOreCountText.text = twooreCount.ToString();
        threeoreCount = inventory.GetOneThreesCount();
        threeOreCountText.text = threeoreCount.ToString();

        oneOreSlider.maxValue = oneoreCount;
        twoOreSlider.maxValue = twooreCount;
        threeOreSlider.maxValue = threeoreCount;
    }
    int oneOre = 10;
    int twoOre = 25;
    int threeore = 50;
    private void IncrementOre(ref int selectedOre, Slider oreSlider, int oreValue)
    {
        if (selectedOre < oreSlider.maxValue)
        {
            selectedOre++;
            oreSlider.value = selectedOre;
            AddExp(oreValue);
        }
    }

    private void UpdateOreSelection(ref int selectedOre, Slider oreSlider, int oreValue, TextMeshProUGUI selectText)
    {
        int difference = (int)oreSlider.value - selectedOre;
        int expChange = difference * oreValue;

        if (expChange > 0) AddExp(expChange);
        else if (expChange < 0) SubtractExp(-expChange);

        selectedOre = (int)oreSlider.value;
        selectText.text = selectedOre.ToString();
    }


    private void UpdateOneOre(float value)
    {
        UpdateOreSelection(ref selectedOneOre, oneOreSlider, oneOre, oneOreSelectText);
        //oneOreSlider.value = ViewEXP;
    }

    private void UpdateTwoOre(float value)
    {
        UpdateOreSelection(ref selectedTwoOre, twoOreSlider, twoOre, twoOreSelectText);
        //oneOreSlider.value = ViewEXP;
    }

    private void UpdateThreeOre(float value)
    {
        UpdateOreSelection(ref selectedThreeOre, threeOreSlider, threeore, threeOreSelectText);
        //oneOreSlider.value = ViewEXP;
    }
   
    private void SubtractExp(int amount)
    {
        ViewEXP -= amount;
        while (ViewEXP < 0 && Viewlevel > 0)
        {
            ViewAttack -= 10;
            Viewlevel--;
            ViewEXP += wepon.maxExp;
        }

        expSlider.value = ViewEXP;
        UpdateWeponUp();
    }
    private void AddExp(int amount)
    {
        ViewEXP += amount;
        while (ViewEXP >= wepon.maxExp)
        {
            ViewAttack += 10;
            Viewlevel++;
            ViewEXP -= wepon.maxExp;
        }
        expSlider.value = ViewEXP;
        UpdateWeponUp();
    }
    
}
