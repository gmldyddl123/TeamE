using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

[System.Serializable]
public class SellItems
{
    public ItemData sellItem; 
    public int itemCount;     // 남아 있는 아이템 갯수
    public int itemCellMoney; // 아이템의 가격
}
public class Store : MonoBehaviour
{
    public Transform slotParent;
    public List<SellItems> cellItemList;
    public GameObject CellItemListSlot;
    public ItemCellTap cellTap; // ItemCellTap의 인스턴스를 참조합니다.

    // 새로운 리스트 추가
    public List<SellItemSlot> sellItemSlots = new List<SellItemSlot>();

    private void Start()
    {
        for (int i = 0; i < cellItemList.Count; i++)
        {
            GameObject newSlot = Instantiate(CellItemListSlot, slotParent);
            SellItemSlot sellItemSlotInstance = newSlot.GetComponent<SellItemSlot>();
            sellItemSlotInstance.cellTap = cellTap;
            sellItemSlotInstance.SetSlot(newSlot, cellItemList[i].sellItem.icon, cellItemList[i].itemCount, cellItemList[i].itemCellMoney, cellItemList[i]);

            // 생성된 슬롯을 리스트에 추가
            sellItemSlots.Add(sellItemSlotInstance);
        }
    }

    // 특정 슬롯을 찾아 업데이트하는 메소드
    public void UpdateSpecificSlot(SellItems item, int updatedItemCount, int updatedItemCellMoney)
    {
        foreach (var slot in sellItemSlots)
        {
            if (slot.currentSellItem == item)
            {
                slot.UpdateSlot(updatedItemCount, updatedItemCellMoney);
                break;
            }
        }
    }
}
