using monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropController : MonoBehaviour
{
    /// <summary>
    /// 테스트용
    /// </summary>
    //public ItemAddTest itemAdd;
    public Inventory inventory;
    public ItemObject itemObject;
    /// <summary>
    /// 랜덤하게 떨어질 아이템들
    /// </summary>
    public ItemData[] allItems;
    /// <summary>
    /// 최대로 떨어질 아이템 갯수
    /// </summary>
    public int maxDropItemCount = 6;
    /// <summary>
    /// 최소 재료 아이템 개수
    /// </summary>
    public int minMaterialCount = 2;
    // 최대 재료 아이템 개수
    public int maxMaterialCount = 5;

    // 아이템 드롭 로직이 실행될 때 호출되는 함수
    public void RandomDropItems()
    {
        // 떨어질 재료 아이템의 개수 결정 (최소 ~ 최대)
        int materialCount = Random.Range(minMaterialCount, maxMaterialCount + 1);

        // 선택된 아이템을 저장할 HashSet (중복 제거)
        HashSet<ItemData> selectedMaterials = new HashSet<ItemData>();

        // 랜덤하게 아이템 선택
        while (selectedMaterials.Count < materialCount)
        {
            int randomIndex = Random.Range(0, allItems.Length);
            ItemData randomItem = allItems[randomIndex];

            selectedMaterials.Add(randomItem);
        }

        // 선택된 재료 아이템을 인벤토리에 추가
        foreach (ItemData item in selectedMaterials)
        {
            // 인벤토리에 아이템 추가 로직
            inventory.Add(item);
        }
    }
}