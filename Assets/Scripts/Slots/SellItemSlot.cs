using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SellItemSlot : MonoBehaviour
{
    public Image itemImg;               // ������ ǥ�ÿ� �̹���
    public TextMeshProUGUI itemPrice;   // ������ ����
    public TextMeshProUGUI CellitemCount;   // ���� ������ ����
    Button SetButton;
    [HideInInspector]
    public SellItems currentSellItem;  // ������ SellItems ����
    public ItemCellTap cellTap;

    private void Awake()
    {
        SetButton = GetComponent<Button>();
        SetButton.onClick.AddListener(OnButtonClicked); // ��ư Ŭ�� �̺�Ʈ�� �޼ҵ� ����
    }

    public void SetSlot(GameObject slot, Sprite itemIcon, int itemCount, int itemCellMoney, SellItems cellItemList)
    {
        if (cellItemList != null && cellItemList.sellItem != null)
        {
            currentSellItem = cellItemList; // ������ SellItems ���� ����
            slot.SetActive(true);
            itemImg.sprite = cellItemList.sellItem.icon;
            CellitemCount.text = "���� ���� : " + itemCount.ToString();
            itemPrice.text = itemCellMoney.ToString();
        }
        else
        {
            slot.SetActive(false);
            itemIcon = null;
        }
    }

    public void UpdateSlot(int updatedItemCount, int updatedItemCellMoney)
    {
        currentSellItem.itemCount = updatedItemCount; // Update the current item count
        CellitemCount.text = "���� ���� : " + updatedItemCount.ToString(); // UI ������Ʈ
        itemPrice.text = updatedItemCellMoney.ToString(); // If needed update the price display as well
    }

    private void OnButtonClicked() // ��ư Ŭ�� �̺�Ʈ ó�� �޼ҵ�
    {
        cellTap.OpenItemCellTap(currentSellItem); // ItemCellTap â ����
    }
}
