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
        moneyText.text = Inventory.instance.Money.ToString();
        Inventory.instance.OnMoneyChanged += UpdateMoneyText;
    }

    void UpdateMoneyText(int amount)
    {
        moneyText.text = amount.ToString();
    }

    private void OnDestroy()
    {
        Inventory.instance.OnMoneyChanged -= UpdateMoneyText;
    }
}