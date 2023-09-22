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
    public Item_FoodItem foodMaterialItem; // 재료로 사용될 FoodMaterial 타입의 아이템
    public int quantity; // 필요한 재료의 갯수
}
[CreateAssetMenu(menuName = "Inventory/Item_FoodItem", fileName = "Item_FoodItem", order = 2)]
public class Item_FoodItem : ItemData
{
    public float plusHP;
    public ItemFoodType foodType;

    // 만약 현재 아이템이 Food 타입이라면, 이 아이템을 만들기 위해 필요한 FoodMaterial 목록을 가질 것입니다.
    public List<Ingredient> requiredIngredients;

    // 생성자나 초기화 메서드에서 requiredIngredients 리스트를 초기화해줄 수 있습니다.
    public Item_FoodItem()
    {
        requiredIngredients = new List<Ingredient>();
    }
}
