using UnityEngine;
using System.Collections.Generic;
public class ItemDropController : MonoBehaviour
{
    public Inventory inventory;
    public ItemData[] allItems;

    public int maxDropItemCount = 6;
    public int minMaterialCount = 2;
    public int maxMaterialCount = 5;

    private const float weaponDropChance = 20f; // 무기 드랍 확률을 20%로 설정합니다.

    public void RandomDropItems()
    {
        int materialCount = Random.Range(minMaterialCount, maxMaterialCount + 1);
        HashSet<ItemData> selectedMaterials = new HashSet<ItemData>();
        bool weaponDropped = false; // 무기가 이미 드랍되었는지 여부를 확인합니다.

        // 무기 드랍 확률을 계산합니다.
        if (Random.Range(0f, 100f) < weaponDropChance)
        {
            ItemData weaponItem = DetermineWeaponDrop(); // 무기를 드랍합니다.
            if (weaponItem != null)
            {
                selectedMaterials.Add(weaponItem); // 드랍된 무기를 추가합니다.
                weaponDropped = true; // 무기가 드랍되었음을 표시합니다.
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
        foreach (ItemData item in selectedMaterials)
        {
            inventory.Add(item);
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

        float randomPoint = Random.Range(0f, totalChance);

        foreach (var weapon in weapons)
        {
            if (randomPoint < weapon.gradeDropChances[weapon.itemgrade])
            {
                return weapon;
            }
            randomPoint -= weapon.gradeDropChances[weapon.itemgrade];
        }

        return null;
    }
}