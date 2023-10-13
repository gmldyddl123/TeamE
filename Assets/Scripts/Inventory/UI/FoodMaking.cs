using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FoodMaking : MonoBehaviour
{
    Image foodItemIcon;                 // ���� ���� ������
    TextMeshProUGUI MakingFoodCount;    // ���� ������ ����
    Slider MakingFoodCountSlider;       // �ִ�ġ�� ���� ���� ������ �ִ� ���������� ���� �� �ִ� ���� ����(�ϳ��� �����ϸ� �� ����)
    Button MakingFood;                  // Ŭ���� ���� ����(����µ� ���� ��� ������ ����) 
    GameObject fooMaterial1;
    GameObject fooMaterial2;
    GameObject fooMaterial3;
    public Image foodMaterialIcon1;     // ��� ������ ������ 
    public Image foodMaterialIcon2;
    public Image foodMaterialIcon3;
    public TextMeshProUGUI currentMaterial1; // ���� ���� ������ �ִ� ������ ��� ���� ǥ��
    public TextMeshProUGUI currentMaterial2;
    public TextMeshProUGUI currentMaterial3;
    public TextMeshProUGUI requiredMaterial1;  // ������ �����ϴµ� �ʿ��� ������ ���� ǥ��
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
        // ��� fooMaterial ��Ȱ��ȭ
        fooMaterial1.gameObject.SetActive(false);
        fooMaterial2.gameObject.SetActive(false);
        fooMaterial3.gameObject.SetActive(false);

        // ��� ���� ������Ʈ
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

        // ���� ������ �ִ� ������ ������
        int creatableCount = Mathf.FloorToInt(MakingFoodCountSlider.value);
        if (creatableCount <= 0) return;

        // ��� ���
        foreach (Ingredient ingredient in currentFoodItem.requiredIngredients)
        {
            for (int i = 0; i < ingredient.quantity * creatableCount; i++)
            {
                Inventory.instance.RemoveItems(ingredient.foodMaterialItem.id, 1);
            }
        }

        // ���� ����
        for (int i = 0; i < creatableCount; i++)
        {
            Inventory.instance.Add(currentFoodItem);
        }

        UpdateFoodMaterials();
        MakingFoodCountSlider.value = 0;
        MakingFoodCountSlider.maxValue = GetMaxCreatableFoodCount();
    }
}
