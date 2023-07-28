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

    Inventory inventory;

    private void Awake()
    {
        inventory = GetComponent<Inventory>();
    }
    private void Start()
    {
    }

    /// <summary>
    /// 필요한 모든 재료가 충분한지 확인하는 함수
    /// </summary>
    /// <returns></returns>
    //private bool HasEnoughIngredients()
    //{
    //    필요한 재료와 현재 가지고 있는 재료 갯수를 비교하여 확인
    //    for (int i = 0; i < foodItem.ingredientId.Length; i++)
    //    {
    //        int gredientId = foodItem.ingredientId[i];
    //        int requiredCount = foodItem.ingredientCounts[i];

    //        필요한 재료보다 부족한 경우
    //        if (exslot.itemCount < requiredCount)
    //        {
    //            return false;
    //        }
    //    }
    //    return true;
    //}

    /// <summary>
    /// 음식을 만들기 위해 재료를 소비하는 함수
    /// </summary>
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

    /// <summary>
    /// 실제 음식을 만드는 함수
    /// </summary>
    //private void CookFood(ItemData _item)
    //{
    //    inventory.Add(_item);
    //    Debug.Log("음식이 만들어졌습니다!");
    //}
    
    public void Get(Item_FoodItem _item)
    {
        foodName.text = _item.named;
        //currentHold.text = foodSlot._item.count;
        foodDescription.text = _item.itemDescription;
        foodSprite.sprite = _item.icon;
    }
}