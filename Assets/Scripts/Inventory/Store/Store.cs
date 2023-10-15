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
    public int itemCount;     // ���� �ִ� ������ ����
    public int itemCellMoney; // �������� ����
}
public class Store : MonoBehaviour
{
    public Transform slotParent;
    public List<SellItems> cellItemList;
    public GameObject CellItemListSlot;
    public ItemCellTap cellTap; // ItemCellTap�� �ν��Ͻ��� �����մϴ�.
    public List<SellItemSlot> sellItemSlots = new List<SellItemSlot>();

    private void Start()
    {
        for (int i = 0; i < cellItemList.Count; i++)
        {

            GameObject newSlot = Instantiate(CellItemListSlot, slotParent);
            SellItemSlot sellItemSlotInstance = newSlot.GetComponent<SellItemSlot>();
            sellItemSlotInstance.cellTap = cellTap;
            sellItemSlotInstance.SetSlot(newSlot, cellItemList[i].sellItem.icon, cellItemList[i].itemCount, cellItemList[i].itemCellMoney, cellItemList[i]);
            sellItemSlots.Add(sellItemSlotInstance);
        }
    }
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
