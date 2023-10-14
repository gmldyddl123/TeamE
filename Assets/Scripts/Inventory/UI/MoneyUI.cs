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
    }
}
