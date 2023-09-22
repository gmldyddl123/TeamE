using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCellTap : MonoBehaviour
{
    public TMP_InputField itemCountInputField; // 인풋필드 참조
    public Store store; // Store 참조
    public SellItemSlot sellItemSlot; // SellItemSlot 참조
    private SellItems selectedItem; // 현재 선택한 아이템
    public TextMeshProUGUI CellItemCount;
    public Button CellItem;
    void Start()
    {
        itemCountInputField.contentType = TMP_InputField.ContentType.IntegerNumber; // 숫자만 입력받도록 설정
        itemCountInputField.onValueChanged.AddListener(OnValueChanged); // 값이 변경될 때마다 OnValueChanged 호출
        CellItem.onClick.AddListener(PurchaseItem); // CellItem 버튼에 PurchaseItem 메서드 연결
    }
    public void OnValueChanged(string value)
    {
        int inputNumber;
        if (int.TryParse(value, out inputNumber)) // 입력값이 숫자인 경우
        {
            if (inputNumber > selectedItem.itemCount) // 입력값이 구매 가능한 총 수량을 초과한 경우
            {
                itemCountInputField.text = selectedItem.itemCount.ToString(); // 자동으로 최대치로 설정
            }
        }
    }
    public void OpenItemCellTap(SellItems sellItems)
    {
        selectedItem = sellItems;

        gameObject.SetActive(true);
        CellItemCount.text = "총 수량 : " + selectedItem.itemCount.ToString();
    }

    public void PurchaseItem()
    {
        int purchaseCount;
        if (int.TryParse(itemCountInputField.text, out purchaseCount) && purchaseCount <= selectedItem.itemCount)
        {
            int totalCost = purchaseCount * selectedItem.itemCellMoney; // 가정: selectedItem.itemCellMoney 가 아이템의 가격을 나타냄
            if (Inventory.instance.Money >= totalCost) // 사용자의 돈이 충분한지 확인. 가정: store.Money가 사용자의 돈을 나타냄
            {
                Inventory.instance.Money -= totalCost; 
                //Inventory.instance.Add(selectedItem); 
                selectedItem.itemCount -= purchaseCount;
                sellItemSlot.UpdateSlot(selectedItem.itemCount, selectedItem.itemCellMoney);

                gameObject.SetActive(false); // ItemCellTap 창을 비활성화
            }
            else
            {
                Debug.Log("Not enough money!"); // 충분한 돈이 없을 경우 로그 출력
            }
        }
    }

}
