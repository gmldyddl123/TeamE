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
    public TextMeshProUGUI noMoney;
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
                //Inventory.instance.Add(selectedItem); 
                Inventory.instance.Money -= totalCost;
                for (int i = 0; i < purchaseCount; i++)
                {
                    Inventory.instance.Add(selectedItem.sellItem);
                }
                selectedItem.itemCount -= purchaseCount;
                store.UpdateSpecificSlot(selectedItem, selectedItem.itemCount, selectedItem.itemCellMoney);
                gameObject.SetActive(false); // ItemCellTap â�� ��Ȱ��ȭ
            }
            else
            {
                Debug.Log("Not enough money!"); // ����� ���� ���� ��� �α� ���
                NoMoney();
            }
        }
    }
    public void NoMoney()
    {
        StartCoroutine(FadeNoMoneyText());
    }

    IEnumerator FadeNoMoneyText()
    {
        noMoney.color = new Color(noMoney.color.r, noMoney.color.g, noMoney.color.b, 0); // ���ĸ� 0���� �����մϴ�.

        // ���İ��� ���������� 1�� �����մϴ�.
        while (noMoney.color.a < 1.0f)
        {
            Color newColor = new Color(noMoney.color.r, noMoney.color.g, noMoney.color.b, noMoney.color.a + (Time.deltaTime / 0.5f));
            noMoney.color = newColor;
            yield return null; // ���� �����ӱ��� ��ٸ��ϴ�.
        }
       
        yield return new WaitForSeconds(1); // 1�� ���� ��ٸ��ϴ�.

        // ���İ��� ���������� 0���� �����մϴ�.
        while (noMoney.color.a > 0.0f)
        {
            Color newColor = new Color(noMoney.color.r, noMoney.color.g, noMoney.color.b, noMoney.color.a - (Time.deltaTime / 0.5f));
            noMoney.color = newColor;
            yield return null; // ���� �����ӱ��� ��ٸ��ϴ�.
        }
    }
}
