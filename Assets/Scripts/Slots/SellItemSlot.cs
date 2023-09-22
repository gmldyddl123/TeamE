using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SellItemSlot : MonoBehaviour
{
    public Image itemImg;               // 아이템 표시용 이미지
    public TextMeshProUGUI itemPrice;   // 아이템 가격
    public TextMeshProUGUI CellitemCount;   // 남은 아이템 갯수
    Button SetButton;
    private SellItems currentSellItem;  // 현재의 SellItems 정보
    public ItemCellTap cellTap;

    private void Awake()
    {
        SetButton = GetComponent<Button>();
        SetButton.onClick.AddListener(OnButtonClicked); // 버튼 클릭 이벤트에 메소드 연결
    }

    public void SetSlot(GameObject slot, Sprite itemIcon, int itemCount, int itemCellMoney, SellItems cellItemList)
    {
        if (cellItemList != null && cellItemList.sellItem != null)
        {
            currentSellItem = cellItemList; // 현재의 SellItems 정보 저장
            slot.SetActive(true);
            itemImg.sprite = cellItemList.sellItem.icon;
            CellitemCount.text = "남은 갯수 : " + itemCount.ToString();
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
        CellitemCount.text = "남은 갯수 : " + updatedItemCount.ToString();
        itemPrice.text = updatedItemCellMoney.ToString();
    }

    private void OnButtonClicked() // 버튼 클릭 이벤트 처리 메소드
    {
        cellTap.OpenItemCellTap(currentSellItem); // ItemCellTap 창 띄우기
    }
}
