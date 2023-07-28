using monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropController : MonoBehaviour
{
    /// <summary>
    /// 테스트용
    /// </summary>
    public ItemAddTest itemAdd;
    public Inventory inventory;
    /// <summary>
    /// 랜덤하게 떨어질 아이템들
    /// </summary>
    public ItemData[] allItems;
    /// <summary>
    /// 최대로 떨어질 아이템 갯수
    /// </summary>
    public int maxDropItemCount = 6;
    /// <summary>
    /// 
    /// </summary>
    public int minMaterialCount = 2; // 최소 재료 아이템 개수를 2로 설정s
    private void Start()
    {
        itemAdd.OnItemDrop += OnItemDropHandler;
    }
    // 몬스터가 죽을 때 실행되는 함수
    private void OnItemDropHandler()
    {
        // 중복 없는 랜덤한 아이템 종류를 선택
        List<ItemData> randomItems = GetRandomItemTypes();

        foreach (ItemData item in randomItems)
        {
            // 선택한 랜덤 아이템을 인벤토리에 추가
            if (item != null)
            {
                // 장비 아이템인 경우 랜덤 등급에 따라 생성
                if (item.itemType == ItemType.equipment)
                {
                    ItemData newItem = GetRandomEquipmentWithGrade(item);
                    if (newItem != null)
                    {
                        inventory.Add(newItem);
                    }
                }
                // 재료 아이템인 경우 최대 10개까지 랜덤하게 생성
                else if (item.itemType == ItemType.expendables)
                {
                    int itemCount = Random.Range(minMaterialCount, Mathf.Min(maxDropItemCount, 10) + 1); // 최대 10개까지만 생성하도록 제한
                    for (int i = 0; i < itemCount; i++)
                    {
                        inventory.Add(item);
                    }
                }
            }
        }
    }

    // 중복 없는 랜덤한 아이템 종류를 선택합니다.
    private List<ItemData> GetRandomItemTypes()
    {
        List<ItemData> randomItems = new List<ItemData>();

        if (allItems.Length == 0)
        {
            Debug.LogWarning("아이템 목록이 비어 있습니다.");
            return randomItems;
        }

        // 랜덤 아이템 종류 선택을 위한 복사 배열 생성
        ItemData[] copyAllItems = new ItemData[allItems.Length];
        allItems.CopyTo(copyAllItems, 0);

        // 최대 5개까지 서로 다른 아이템 종류 선택
        int itemCount = Mathf.Min(maxDropItemCount, allItems.Length);
        for (int i = 0; i < itemCount; i++)
        {
            int randomIndex = Random.Range(0, copyAllItems.Length);
            ItemData randomItem = copyAllItems[randomIndex];
            if (randomItem != null)
            {
                randomItems.Add(randomItem);
                copyAllItems[randomIndex] = null; // 이미 선택한 아이템은 중복되지 않도록 null로 처리
            }
        }

        return randomItems;
    }
    // 랜덤 등급의 장비 아이템을 생성합니다.
    private ItemData GetRandomEquipmentWithGrade(ItemData baseItem)
    {
        if (baseItem == null || baseItem.itemType != ItemType.equipment)
            return null;

        // 등급에 따른 확률 계산
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
                // 해당 등급의 아이템 생성
                ItemData newItem = ScriptableObject.CreateInstance<ItemData>();
                newItem.itemgrade = grade;
                newItem.itemType = baseItem.itemType;
                newItem.named = baseItem.named;
                newItem.icon = baseItem.icon;
                return newItem;
            }
            randomValue -= gradeProbability;
        }
        return null; // 등급 확률에 해당하는 아이템이 없을 경우 null 반환
    }
}