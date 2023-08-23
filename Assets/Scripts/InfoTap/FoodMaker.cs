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
    /// ������ �̸�
    /// </summary>
    public TextMeshProUGUI foodName;
    /// <summary>
    /// ������ ���� ����
    /// </summary>
    public TextMeshProUGUI currentHold;
    /// <summary>
    /// ������ ����
    /// </summary>
    public TextMeshProUGUI foodDescription;
    /// <summary>
    /// ������ �� ����
    /// </summary>
    //public TextMeshProUGUI FoodStar;

    Item_FoodItem foodItem;

    //private void Start()
    //{
    //}

    ///// <summary>
    ///// �ʿ��� ��� ��ᰡ ������� Ȯ���ϴ� �Լ�
    ///// </summary>
    ///// <returns></returns>
    //private bool HasEnoughIngredients()
    //{
    //    //�ʿ��� ���� ���� ������ �ִ� ��� ������ ���Ͽ� Ȯ��
    //    for (int i = 0; i < foodItem.ingredientId.Length; i++)
    //    {
    //        int gredientId = foodItem.ingredientId[i];
    //        int requiredCount = foodItem.ingredientCounts[i];

    //        //�ʿ��� ��Ẹ�� ������ ���
    //        if (exslot.itemCount < requiredCount)
    //        {
    //            return false;
    //        }
    //    }
    //    return true;
    //}

    ///// <summary>
    ///// ������ ����� ���� ��Ḧ �Һ��ϴ� �Լ�
    ///// </summary>
    //private void ConsumeIngredients()
    //{
    //    // ���� ����⿡ �ʿ��� ������ ������ ���ҽ�Ŵ
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
    ///// ���� ������ ����� �Լ�
    ///// </summary>
    //private void CookFood(ItemData _item)
    //{
    //    inventory.Add(_item);
    //    Debug.Log("������ ����������ϴ�!");
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
    //        Debug.Log("������ ����� ���� ��ᰡ ������� �ʽ��ϴ�.");
    //    }
    //}

    //private bool HasRequiredIngredients(Item_FoodItem foodItem)
    //{
    //    for (int i = 0; i < MaxArraySize; i++)
    //    {
    //        int ingredientId = foodItem.ingredientId[i];
    //        int ingredientCount = foodItem.ingredientCounts[i];

    //        if (ingredientId == 0 || ingredientCount == 0) // 0�� ��ᰡ ������ �ǹ�
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

    //        if (ingredientId == 0 || ingredientCount == 0) // 0�� ��ᰡ ������ �ǹ�
    //        {
    //            break;
    //        }

    //        for (int j = 0; j < ingredientCount; j++)
    //        {
    //            inventory.Remove(ingredientId);
    //        }
    //    }

    //    // ���� ������ �߰�
    //    inventory.Add(foodItem.id);

    //    Debug.Log(foodItem.name + "��(��) ���۵Ǿ����ϴ�.");
    //}

    public void Get(Item_FoodItem _item)
    {
        foodName.text = _item.named;
        //currentHold.text = foodSlot._item.count;
        foodDescription.text = _item.itemDescription;
        foodSprite.sprite = _item.icon;
    }
}