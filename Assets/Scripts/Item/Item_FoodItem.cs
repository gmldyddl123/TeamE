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
    /// ������� �����۵��� ���̵� �迭
    /// </summary>
    public int[] ingredientId = new int[4];
    /// <summary>
    /// ������� �����۵��� ���� �迭 (ingredientNames �迭�� �ε��� ������ ����� ��)
    /// </summary>
    public int[] ingredientCounts = new int[4];
    private void OnValidate()
    {
        // �迭 ũ�Ⱑ �ִ� ũ�⸦ ���� �ʵ��� ����
        if (ingredientId.Length > MaxArraySize)
        {
            Debug.LogWarning("�迭 ũ��� �ִ� " + MaxArraySize + "������ �����մϴ�.");
            System.Array.Resize(ref ingredientId, MaxArraySize);
        }
        if (ingredientCounts.Length > MaxArraySize)
        {
            Debug.LogWarning("�迭 ũ��� �ִ� " + MaxArraySize + "������ �����մϴ�.");
            System.Array.Resize(ref ingredientCounts, MaxArraySize);
        }
    }
}
