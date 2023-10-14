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

    private void Start()
    {
        for (int i = 0; i < cellItemList.Count; i++)
        {
            GameObject newSlot = Instantiate(CellItemListSlot, slotParent);
            SellItemSlot sellItemSlotInstance = newSlot.GetComponent<SellItemSlot>(); // ���� ������ �ν��Ͻ����� SellItemSlot�� ����ϴ�.
            sellItemSlotInstance.cellTap = cellTap; // SellItemSlot�� cellTap�� ItemCellTap�� �ν��Ͻ��� �Ҵ��մϴ�.
            sellItemSlotInstance.SetSlot(newSlot, cellItemList[i].sellItem.icon, cellItemList[i].itemCount, cellItemList[i].itemCellMoney, cellItemList[i]);
        }
    }
}
