using UnityEngine;
using System.Collections.Generic;
public class ItemDropController : MonoBehaviour
{
    public Inventory inventory;
    public ItemData[] allItems;

    public int maxDropItemCount = 6;
    public int minMaterialCount = 2;
    public int maxMaterialCount = 5;

    private const float weaponDropChance = 20f; // ���� ��� Ȯ���� 20%�� �����մϴ�.

    public void RandomDropItems()
    {
        int materialCount = Random.Range(minMaterialCount, maxMaterialCount + 1);
        HashSet<ItemData> selectedMaterials = new HashSet<ItemData>();
        bool weaponDropped = false; // ���Ⱑ �̹� ����Ǿ����� ���θ� Ȯ���մϴ�.

        // ���� ��� Ȯ���� ����մϴ�.
        if (Random.Range(0f, 100f) < weaponDropChance)
        {
            ItemData weaponItem = DetermineWeaponDrop(); // ���⸦ ����մϴ�.
            if (weaponItem != null)
            {
                selectedMaterials.Add(weaponItem); // ����� ���⸦ �߰��մϴ�.
                weaponDropped = true; // ���Ⱑ ����Ǿ����� ǥ���մϴ�.
            }
        }

        while (selectedMaterials.Count < materialCount)
        {
            int randomIndex = Random.Range(0, allItems.Length);
            ItemData randomItem = allItems[randomIndex];
            if (!weaponDropped || !(randomItem is Item_WeaponData))
            {
                bool added = selectedMaterials.Add(randomItem);
                if (added && randomItem is Item_WeaponData)
                {
                    weaponDropped = true;
                }
            }
        }

        // ���⼭���ʹ� �� ��� �����Դϴ�.
        int moneyDrop = Random.Range(10, 101); // 10���� 100 ������ ������ ���� �����մϴ�.
        inventory.Money += moneyDrop; // ������ ������ ���� ���� ���� �߰��մϴ�.

        foreach (ItemData item in selectedMaterials)
        {
            inventory.Add(item.Clone()); // �� �ν��Ͻ��� �߰��մϴ�.
        }
    }


    private ItemData DetermineWeaponDrop()
    {
        List<ItemData> weapons = new List<ItemData>();
        foreach (var item in allItems)
        {
            if (item is Item_WeaponData)
            {
                weapons.Add(item);
            }
        }

        if (weapons.Count == 0) return null;

        float totalChance = 0;
        foreach (var weapon in weapons)
        {
            totalChance += weapon.gradeDropChances[weapon.itemgrade];
        }

        // "���Ⱑ ���õ��� ����"�� Ȯ���� �߰��մϴ�.
        float noWeaponChance = 80f; // ���Ⱑ ���õ��� ���� Ȯ���� 90

        float randomPoint = Random.Range(0, 100);

        // ���� "���Ⱑ ���õ��� ����"�� ��츦 ó���մϴ�.
        if (randomPoint < noWeaponChance)
        {
            return null; // ���Ⱑ ������� �ʽ��ϴ�.
        }

        // ���� Ȯ������ ���⸦ �����մϴ�.
        randomPoint -= noWeaponChance; // "���Ⱑ ���õ��� ����"�� Ȯ���� �����մϴ�.

        foreach (var weapon in weapons)
        {
            if (randomPoint < weapon.gradeDropChances[weapon.itemgrade])
            {
                return weapon;
            }
            randomPoint -= weapon.gradeDropChances[weapon.itemgrade];
        }

        return null; // �� �ڵ忡 �����ϸ�, ��� ���⵵ ���õ��� �ʾ����� �ǹ��մϴ�.
    }

}