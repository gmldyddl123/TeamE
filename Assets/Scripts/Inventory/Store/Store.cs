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
    public SellItemSlot FindSellItemSlot(SellItems itemToFind)
    {
        foreach (Transform child in slotParent)
        {
            SellItemSlot slot = child.GetComponent<SellItemSlot>();
            if (slot != null && slot.currentSellItem == itemToFind)
            {
                return slot;
            }
        }
        return null;
    }

    private void Start()
    {
        for (int i = 0; i < cellItemList.Count; i++)
        {
            GameObject newSlot = Instantiate(CellItemListSlot, slotParent);
            SellItemSlot sellItemSlotInstance = newSlot.GetComponent<SellItemSlot>(); // 새로 생성된 인스턴스에서 SellItemSlot을 얻습니다.
            sellItemSlotInstance.cellTap = cellTap; // SellItemSlot의 cellTap에 ItemCellTap의 인스턴스를 할당합니다.
            sellItemSlotInstance.SetSlot(newSlot, cellItemList[i].sellItem.icon, cellItemList[i].itemCount, cellItemList[i].itemCellMoney, cellItemList[i]);
        }
    }
}
