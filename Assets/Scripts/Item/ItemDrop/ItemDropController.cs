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

        // 여기서부터는 돈 드랍 로직입니다.
        int moneyDrop = Random.Range(10, 101); // 10에서 100 사이의 랜덤한 값을 생성합니다.
        inventory.Money += moneyDrop; // 생성된 랜덤한 값을 현재 돈에 추가합니다.

        foreach (ItemData item in selectedMaterials)
        {
            inventory.Add(item.Clone()); // 새 인스턴스를 추가합니다.
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

        // "무기가 선택되지 않음"의 확률을 추가합니다.
        float noWeaponChance = 80f; // 무기가 선택되지 않을 확률은 90

        float randomPoint = Random.Range(0, 100);

        // 먼저 "무기가 선택되지 않음"의 경우를 처리합니다.
        if (randomPoint < noWeaponChance)
        {
            return null; // 무기가 드랍되지 않습니다.
        }

        // 남은 확률에서 무기를 선택합니다.
        randomPoint -= noWeaponChance; // "무기가 선택되지 않음"의 확률을 제거합니다.

        foreach (var weapon in weapons)
        {
            if (randomPoint < weapon.gradeDropChances[weapon.itemgrade])
            {
                return weapon;
            }
            randomPoint -= weapon.gradeDropChances[weapon.itemgrade];
        }

        return null; // 이 코드에 도달하면, 어떠한 무기도 선택되지 않았음을 의미합니다.
    }

}