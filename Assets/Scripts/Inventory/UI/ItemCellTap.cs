using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCellTap : MonoBehaviour
{
    public TMP_InputField itemCountInputField; // ��ǲ�ʵ� ����
    public Store store; // Store ����
    public SellItemSlot sellItemSlot; // SellItemSlot ����
    private SellItems selectedItem; // ���� ������ ������
    public TextMeshProUGUI CellItemCount;
    public Button CellItem;
    void Start()
    {
        itemCountInputField.contentType = TMP_InputField.ContentType.IntegerNumber; // ���ڸ� �Է¹޵��� ����
        itemCountInputField.onValueChanged.AddListener(OnValueChanged); // ���� ����� ������ OnValueChanged ȣ��
        CellItem.onClick.AddListener(PurchaseItem); // CellItem ��ư�� PurchaseItem �޼��� ����
    }
    public void OnValueChanged(string value)
    {
        int inputNumber;
        if (int.TryParse(value, out inputNumber)) // �Է°��� ������ ���
        {
            if (inputNumber > selectedItem.itemCount) // �Է°��� ���� ������ �� ������ �ʰ��� ���
            {
                itemCountInputField.text = selectedItem.itemCount.ToString(); // �ڵ����� �ִ�ġ�� ����
            }
        }
    }
    public void OpenItemCellTap(SellItems sellItems)
    {
        selectedItem = sellItems;

        gameObject.SetActive(true);
        CellItemCount.text = "�� ���� : " + selectedItem.itemCount.ToString();
    }

    public void PurchaseItem()
    {
        int purchaseCount;
        if (int.TryParse(itemCountInputField.text, out purchaseCount) && purchaseCount <= selectedItem.itemCount)
        {
            int totalCost = purchaseCount * selectedItem.itemCellMoney; // ����: selectedItem.itemCellMoney �� �������� ������ ��Ÿ��
            if (Inventory.instance.Money >= totalCost) // ������� ���� ������� Ȯ��. ����: store.Money�� ������� ���� ��Ÿ��
            {
                Inventory.instance.Money -= totalCost; 
                //Inventory.instance.Add(selectedItem); 
                selectedItem.itemCount -= purchaseCount;
                sellItemSlot.UpdateSlot(selectedItem.itemCount, selectedItem.itemCellMoney);

                gameObject.SetActive(false); // ItemCellTap â�� ��Ȱ��ȭ
            }
            else
            {
                Debug.Log("Not enough money!"); // ����� ���� ���� ��� �α� ���
            }
        }
    }

}
