using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FoodMaker : MonoBehaviour
{
    public Image foodSprite;
    /// <summary>
    /// 음식의 이름
    /// </summary>
    public TextMeshProUGUI foodName;
    /// <summary>
    /// 음식의 남은 갯수
    /// </summary>
    public TextMeshProUGUI currentHold;
    /// <summary>
    /// 음식의 설명
    /// </summary>
    public TextMeshProUGUI foodDescription;
    /// <summary>
    /// 음식의 별 갯수
    /// </summary>
    //public TextMeshProUGUI FoodStar;

    Item_FoodItem foodItem;

    //private void Start()
    //{
    //}

    ///// <summary>
    ///// 필요한 모든 재료가 충분한지 확인하는 함수
    ///// </summary>
    ///// <returns></returns>
    //private bool HasEnoughIngredients()
    //{
    //    //필요한 재료와 현재 가지고 있는 재료 갯수를 비교하여 확인
    //    for (int i = 0; i < foodItem.ingredientId.Length; i++)
    //    {
    //        int gredientId = foodItem.ingredientId[i];
    //        int requiredCount = foodItem.ingredientCounts[i];

    //        //필요한 재료보다 부족한 경우
    //        if (exslot.itemCount < requiredCount)
    //        {
    //            return false;
    //        }
    //    }
    //    return true;
    //}

    ///// <summary>
    ///// 음식을 만들기 위해 재료를 소비하는 함수
    ///// </summary>
    //private void ConsumeIngredients()
    //{
    //    // 음식 만들기에 필요한 재료들의 갯수를 감소시킴
    //    for (int i = 0; i < foodItem.ingredientId.Length; i++)
    //    {
    //        int gredientId = foodItem.ingredientId[i];
    //        int requiredCount = foodItem.ingredientCounts[i];

    //        if (exslot.itemCount > requiredCount)
    //        {
    //            exslot.itemCount -= requiredCount;
    //        }
    //    }
    //}

    ///// <summary>
    ///// 실제 음식을 만드는 함수
    ///// </summary>
    //private void CookFood(ItemData _item)
    //{
    //    inventory.Add(_item);
    //    Debug.Log("음식이 만들어졌습니다!");
    //}
    //public List<Item_FoodItem> foodItems;
    //public List<int> inventory;

    //public void TryCraftFood(int foodItemId)
    //{
    //    Item_FoodItem targetFood = foodItems.Find(item => item.id == foodItemId);

    //    if (targetFood != null && HasRequiredIngredients(targetFood))
    //    {
    //        CraftFood(targetFood);
    //    }
    //    else
    //    {
    //        Debug.Log("음식을 만들기 위한 재료가 충분하지 않습니다.");
    //    }
    //}

    //private bool HasRequiredIngredients(Item_FoodItem foodItem)
    //{
    //    for (int i = 0; i < MaxArraySize; i++)
    //    {
    //        int ingredientId = foodItem.ingredientId[i];
    //        int ingredientCount = foodItem.ingredientCounts[i];

    //        if (ingredientId == 0 || ingredientCount == 0) // 0은 재료가 없음을 의미
    //        {
    //            break;
    //        }

    //        int countInInventory = inventory.FindAll(id => id == ingredientId).Count;

    //        if (countInInventory < ingredientCount)
    //        {
    //            return false;
    //        }
    //    }

    //    return true;
    //}

    //private void CraftFood(Item_FoodItem foodItem)
    //{
    //    for (int i = 0; i < MaxArraySize; i++)
    //    {
    //        int ingredientId = foodItem.ingredientId[i];
    //        int ingredientCount = foodItem.ingredientCounts[i];

    //        if (ingredientId == 0 || ingredientCount == 0) // 0은 재료가 없음을 의미
    //        {
    //            break;
    //        }

    //        for (int j = 0; j < ingredientCount; j++)
    //        {
    //            inventory.Remove(ingredientId);
    //        }
    //    }

    //    // 음식 아이템 추가
    //    inventory.Add(foodItem.id);

    //    Debug.Log(foodItem.name + "이(가) 제작되었습니다.");
    //}

    public void Get(Item_FoodItem _item)
    {
        foodName.text = _item.named;
        //currentHold.text = foodSlot._item.count;
        foodDescription.text = _item.itemDescription;
        foodSprite.sprite = _item.icon;
    }
}