using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FoodMaking : MonoBehaviour
{
    Image foodItemIcon;                 // 만들 음식 아이콘
    TextMeshProUGUI MakingFoodCount;    // 만들 음식의 갯수
    Slider MakingFoodCountSlider;       // 최대치는 내가 현재 가지고 있는 아이템으로 만들 수 있는 음식 갯수(하나라도 부족하면 못 만듬)
    Button MakingFood;                  // 클릭시 음식 생성(만드는데 사용된 재료 아이템 삭제) 
    GameObject fooMaterial1;
    GameObject fooMaterial2;
    GameObject fooMaterial3;
    public Image foodMaterialIcon1;     // 재료 아이템 아이콘 
    public Image foodMaterialIcon2;
    public Image foodMaterialIcon3;
    public TextMeshProUGUI currentMaterial1; // 현재 내가 가지고 있는 아이템 재료 갯수 표시
    public TextMeshProUGUI currentMaterial2;
    public TextMeshProUGUI currentMaterial3;
    public TextMeshProUGUI requiredMaterial1;  // 음식을 제작하는데 필요한 아이템 갯수 표시
    public TextMeshProUGUI requiredMaterial2;
    public TextMeshProUGUI requiredMaterial3;
    private Item_FoodItem currentFoodItem;
    private void Awake()
    {
        Transform child = transform.GetChild(0);
        foodItemIcon = child.GetComponent<Image>();
        child = transform.GetChild(1);
        MakingFoodCount = child.GetComponent<TextMeshProUGUI>();
        child = transform.GetChild(2);
        MakingFoodCountSlider = child.GetComponent<Slider>();
        child = transform.GetChild(3);
        MakingFood = child.GetComponent<Button>();
        child = transform.GetChild(4);
        fooMaterial1 = child.gameObject;
        child = transform.GetChild(5);
        fooMaterial2 = child.gameObject;
        child = transform.GetChild(6);
        fooMaterial3 = child.gameObject;
        MakingFoodCountSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }
    private void OnSliderValueChanged(float value)
    {
        MakingFoodCount.text = Mathf.FloorToInt(value).ToString();
    }
    private void UpdateFoodMaterials()
    {
        // 모든 fooMaterial 비활성화
        fooMaterial1.gameObject.SetActive(false);
        fooMaterial2.gameObject.SetActive(false);
        fooMaterial3.gameObject.SetActive(false);

        // 재료 정보 업데이트
        for (int i = 0; i < currentFoodItem.requiredIngredients.Count; i++)
        {
            Ingredient ingredient = currentFoodItem.requiredIngredients[i];
            int currentCount = Inventory.instance.exItems.Count(item => item == ingredient.foodMaterialItem);
            int requiredCount = ingredient.quantity;

            if (i == 0)
            {
                fooMaterial1.gameObject.SetActive(true);
                foodMaterialIcon1.sprite = ingredient.foodMaterialItem.icon;
                currentMaterial1.text = currentCount.ToString();
                requiredMaterial1.text = requiredCount.ToString();
            }
            else if (i == 1)
            {
                fooMaterial2.gameObject.SetActive(true);
                foodMaterialIcon2.sprite = ingredient.foodMaterialItem.icon;
                currentMaterial2.text = currentCount.ToString();
                requiredMaterial2.text = requiredCount.ToString();
            }
            else if (i == 2)
            {
                fooMaterial3.gameObject.SetActive(true);
                foodMaterialIcon3.sprite = ingredient.foodMaterialItem.icon;
                currentMaterial3.text = currentCount.ToString();
                requiredMaterial3.text = requiredCount.ToString();
            }
        }
    }

    public void FoodGet(ItemData foodItem)
    {
        if (foodItem is Item_FoodItem food)
        {
            currentFoodItem = food;
            MakingFoodCount.text = "0".ToString();
            MakingFoodCountSlider.value = 0;
            foodItemIcon.sprite = food.icon;
            UpdateFoodMaterials();
            MakingFoodCountSlider.maxValue = GetMaxCreatableFoodCount();
            MakingFood.onClick.AddListener(CreateFood);
        }
    }
    private int GetMaxCreatableFoodCount()
    {
        int maxCount = int.MaxValue;

        foreach (Ingredient ingredient in currentFoodItem.requiredIngredients)
        {
            int currentCount = Inventory.instance.exItems.Count(item => item == ingredient.foodMaterialItem);
            maxCount = Mathf.Min(maxCount, currentCount / ingredient.quantity);
        }

        return maxCount;
    }

    private void CreateFood()
    {
        if (currentFoodItem == null) return;

        // 제작 가능한 최대 갯수를 가져옴
        int creatableCount = Mathf.FloorToInt(MakingFoodCountSlider.value);
        if (creatableCount <= 0) return;

        // 재료 사용
        foreach (Ingredient ingredient in currentFoodItem.requiredIngredients)
        {
            for (int i = 0; i < ingredient.quantity * creatableCount; i++)
            {
                Inventory.instance.RemoveItems(ingredient.foodMaterialItem.id, 1);
            }
        }

        // 음식 제작
        for (int i = 0; i < creatableCount; i++)
        {
            Inventory.instance.Add(currentFoodItem);
        }

        UpdateFoodMaterials();
        MakingFoodCountSlider.value = 0;
        MakingFoodCountSlider.maxValue = GetMaxCreatableFoodCount();
    }
}
