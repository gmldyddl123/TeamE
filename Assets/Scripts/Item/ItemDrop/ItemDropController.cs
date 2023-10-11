using monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropController : MonoBehaviour
{
    /// <summary>
    /// �׽�Ʈ��
    /// </summary>
    //public ItemAddTest itemAdd;
    public Inventory inventory;
    public ItemObject itemObject;
    /// <summary>
    /// �����ϰ� ������ �����۵�
    /// </summary>
    public ItemData[] allItems;
    /// <summary>
    /// �ִ�� ������ ������ ����
    /// </summary>
    public int maxDropItemCount = 6;
    /// <summary>
    /// �ּ� ��� ������ ����
    /// </summary>
    public int minMaterialCount = 2;
    // �ִ� ��� ������ ����
    public int maxMaterialCount = 5;

    // ������ ��� ������ ����� �� ȣ��Ǵ� �Լ�
    public void RandomDropItems()
    {
        // ������ ��� �������� ���� ���� (�ּ� ~ �ִ�)
        int materialCount = Random.Range(minMaterialCount, maxMaterialCount + 1);

        // ���õ� �������� ������ HashSet (�ߺ� ����)
        HashSet<ItemData> selectedMaterials = new HashSet<ItemData>();

        // �����ϰ� ������ ����
        while (selectedMaterials.Count < materialCount)
        {
            int randomIndex = Random.Range(0, allItems.Length);
            ItemData randomItem = allItems[randomIndex];

            selectedMaterials.Add(randomItem);
        }

        // ���õ� ��� �������� �κ��丮�� �߰�
        foreach (ItemData item in selectedMaterials)
        {
            // �κ��丮�� ������ �߰� ����
            inventory.Add(item);
        }
    }
}