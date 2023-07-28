using monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropController : MonoBehaviour
{
    /// <summary>
    /// �׽�Ʈ��
    /// </summary>
    public ItemAddTest itemAdd;
    public Inventory inventory;
    /// <summary>
    /// �����ϰ� ������ �����۵�
    /// </summary>
    public ItemData[] allItems;
    /// <summary>
    /// �ִ�� ������ ������ ����
    /// </summary>
    public int maxDropItemCount = 6;
    /// <summary>
    /// 
    /// </summary>
    public int minMaterialCount = 2; // �ּ� ��� ������ ������ 2�� ����s
    private void Start()
    {
        itemAdd.OnItemDrop += OnItemDropHandler;
    }
    // ���Ͱ� ���� �� ����Ǵ� �Լ�
    private void OnItemDropHandler()
    {
        // �ߺ� ���� ������ ������ ������ ����
        List<ItemData> randomItems = GetRandomItemTypes();

        foreach (ItemData item in randomItems)
        {
            // ������ ���� �������� �κ��丮�� �߰�
            if (item != null)
            {
                // ��� �������� ��� ���� ��޿� ���� ����
                if (item.itemType == ItemType.equipment)
                {
                    ItemData newItem = GetRandomEquipmentWithGrade(item);
                    if (newItem != null)
                    {
                        inventory.Add(newItem);
                    }
                }
                // ��� �������� ��� �ִ� 10������ �����ϰ� ����
                else if (item.itemType == ItemType.expendables)
                {
                    int itemCount = Random.Range(minMaterialCount, Mathf.Min(maxDropItemCount, 10) + 1); // �ִ� 10�������� �����ϵ��� ����
                    for (int i = 0; i < itemCount; i++)
                    {
                        inventory.Add(item);
                    }
                }
            }
        }
    }

    // �ߺ� ���� ������ ������ ������ �����մϴ�.
    private List<ItemData> GetRandomItemTypes()
    {
        List<ItemData> randomItems = new List<ItemData>();

        if (allItems.Length == 0)
        {
            Debug.LogWarning("������ ����� ��� �ֽ��ϴ�.");
            return randomItems;
        }

        // ���� ������ ���� ������ ���� ���� �迭 ����
        ItemData[] copyAllItems = new ItemData[allItems.Length];
        allItems.CopyTo(copyAllItems, 0);

        // �ִ� 5������ ���� �ٸ� ������ ���� ����
        int itemCount = Mathf.Min(maxDropItemCount, allItems.Length);
        for (int i = 0; i < itemCount; i++)
        {
            int randomIndex = Random.Range(0, copyAllItems.Length);
            ItemData randomItem = copyAllItems[randomIndex];
            if (randomItem != null)
            {
                randomItems.Add(randomItem);
                copyAllItems[randomIndex] = null; // �̹� ������ �������� �ߺ����� �ʵ��� null�� ó��
            }
        }

        return randomItems;
    }
    // ���� ����� ��� �������� �����մϴ�.
    private ItemData GetRandomEquipmentWithGrade(ItemData baseItem)
    {
        if (baseItem == null || baseItem.itemType != ItemType.equipment)
            return null;

        // ��޿� ���� Ȯ�� ���
        float totalGradeProbability = 0f;
        foreach (var grade in baseItem.gradeDropChances.Keys)
        {
            totalGradeProbability += baseItem.gradeDropChances[grade];
        }

        float randomValue = Random.value * totalGradeProbability;

        foreach (var grade in baseItem.gradeDropChances.Keys)
        {
            float gradeProbability = baseItem.gradeDropChances[grade];
            if (randomValue < gradeProbability)
            {
                // �ش� ����� ������ ����
                ItemData newItem = ScriptableObject.CreateInstance<ItemData>();
                newItem.itemgrade = grade;
                newItem.itemType = baseItem.itemType;
                newItem.named = baseItem.named;
                newItem.icon = baseItem.icon;
                return newItem;
            }
            randomValue -= gradeProbability;
        }
        return null; // ��� Ȯ���� �ش��ϴ� �������� ���� ��� null ��ȯ
    }
}