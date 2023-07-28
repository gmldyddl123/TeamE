using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item_FoodItem", fileName = "Item_FoodItem", order = 3)]
public class Item_FoodItem : ItemData
{
    public float value;
    private const int MaxArraySize = 4;
    /// <summary>
    /// 음식재료 아이템들의 아이디 배열
    /// </summary>
    public int[] ingredientId = new int[4];
    /// <summary>
    /// 음식재료 아이템들의 갯수 배열 (ingredientNames 배열과 인덱스 순서를 맞춰야 함)
    /// </summary>
    public int[] ingredientCounts = new int[4];
    private void OnValidate()
    {
        // 배열 크기가 최대 크기를 넘지 않도록 유지
        if (ingredientId.Length > MaxArraySize)
        {
            Debug.LogWarning("배열 크기는 최대 " + MaxArraySize + "까지만 가능합니다.");
            System.Array.Resize(ref ingredientId, MaxArraySize);
        }
        if (ingredientCounts.Length > MaxArraySize)
        {
            Debug.LogWarning("배열 크기는 최대 " + MaxArraySize + "까지만 가능합니다.");
            System.Array.Resize(ref ingredientCounts, MaxArraySize);
        }
    }
}
