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
        // 초기 UI 텍스트 설정
        moneyText.text = Inventory.instance.Money.ToString();

        // Inventory의 OnMoneyChanged 이벤트에 메서드를 등록합니다.
        Inventory.instance.OnMoneyChanged += UpdateMoneyText;
    }

    void UpdateMoneyText(int amount)
    {
        // 돈의 양이 변경될 때 UI 텍스트를 업데이트합니다.
        moneyText.text = amount.ToString();
    }

    private void OnDestroy()
    {
        // 오브젝트가 파괴될 때 이벤트 구독을 해지합니다.
        Inventory.instance.OnMoneyChanged -= UpdateMoneyText;
    }
}