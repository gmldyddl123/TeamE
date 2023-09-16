using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum ItemFoodType
{
    FoodMaterial,
    Food
}
[System.Serializable]
public class Ingredient
{
    public Item_FoodItem foodMaterialItem; // ���� ���� FoodMaterial Ÿ���� ������
    public int quantity; // �ʿ��� ����� ����
}
[CreateAssetMenu(menuName = "Inventory/Item_FoodItem", fileName = "Item_FoodItem", order = 2)]
public class Item_FoodItem : ItemData
{
    public float plusHP;
    public ItemFoodType foodType;

    // ���� ���� �������� Food Ÿ���̶��, �� �������� ����� ���� �ʿ��� FoodMaterial ����� ���� ���Դϴ�.
    public List<Ingredient> requiredIngredients;

    // �����ڳ� �ʱ�ȭ �޼��忡�� requiredIngredients ����Ʈ�� �ʱ�ȭ���� �� �ֽ��ϴ�.
    public Item_FoodItem()
    {
        requiredIngredients = new List<Ingredient>();
    }
}
