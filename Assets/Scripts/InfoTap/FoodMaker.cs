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

    Inventory inventory;

    private void Awake()
    {
        inventory = GetComponent<Inventory>();
    }
    private void Start()
    {
    }

    /// <summary>
    /// �ʿ��� ��� ��ᰡ ������� Ȯ���ϴ� �Լ�
    /// </summary>
    /// <returns></returns>
    //private bool HasEnoughIngredients()
    //{
    //    �ʿ��� ���� ���� ������ �ִ� ��� ������ ���Ͽ� Ȯ��
    //    for (int i = 0; i < foodItem.ingredientId.Length; i++)
    //    {
    //        int gredientId = foodItem.ingredientId[i];
    //        int requiredCount = foodItem.ingredientCounts[i];

    //        �ʿ��� ��Ẹ�� ������ ���
    //        if (exslot.itemCount < requiredCount)
    //        {
    //            return false;
    //        }
    //    }
    //    return true;
    //}

    /// <summary>
    /// ������ ����� ���� ��Ḧ �Һ��ϴ� �Լ�
    /// </summary>
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

    /// <summary>
    /// ���� ������ ����� �Լ�
    /// </summary>
    //private void CookFood(ItemData _item)
    //{
    //    inventory.Add(_item);
    //    Debug.Log("������ ����������ϴ�!");
    //}
    
    public void Get(Item_FoodItem _item)
    {
        foodName.text = _item.named;
        //currentHold.text = foodSlot._item.count;
        foodDescription.text = _item.itemDescription;
        foodSprite.sprite = _item.icon;
    }
}