using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    TextMeshProUGUI moneyText;

    private void Awake()
    {
        Transform child = transform.GetChild(1);
        moneyText = child.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        // �ʱ� UI �ؽ�Ʈ ����
        moneyText.text = Inventory.instance.Money.ToString();

        // Inventory�� OnMoneyChanged �̺�Ʈ�� �޼��带 ����մϴ�.
        Inventory.instance.OnMoneyChanged += UpdateMoneyText;
    }

    void UpdateMoneyText(int amount)
    {
        // ���� ���� ����� �� UI �ؽ�Ʈ�� ������Ʈ�մϴ�.
        moneyText.text = amount.ToString();
    }

    private void OnDestroy()
    {
        // ������Ʈ�� �ı��� �� �̺�Ʈ ������ �����մϴ�.
        Inventory.instance.OnMoneyChanged -= UpdateMoneyText;
    }
}