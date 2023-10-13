using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public GameObject UI;
    public List<GameObject> Tabs; // 모든 탭을 포함하는 리스트
    private List<Button> buttons; // 모든 버튼을 포함하는 리스트

    private void Awake()
    {
        buttons = new List<Button>();

        // 모든 자식 컴포넌트에서 버튼 컴포넌트를 가져옵니다.
        foreach (Transform child in transform)
        {
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                buttons.Add(button);
            }
        }

        // 버튼에 리스너 추가
        if (buttons.Count >= 5)
        {
            buttons[0].onClick.AddListener(CloseTap); // Close 버튼
            buttons[1].onClick.AddListener(OpenImTap); // Important 탭 버튼
            buttons[2].onClick.AddListener(OpenFoodTap); // Food 탭 버튼
            buttons[3].onClick.AddListener(OpenMaTap); // Material 탭 버튼
            buttons[4].onClick.AddListener(OpenWeaponTap); // Weapon 탭 버튼
        }
    }

    // 모든 탭을 비활성화한 후 지정된 탭만 활성화하는 메서드
    private void OpenTab(GameObject tab)
    {
        foreach (var item in Tabs)
        {
            item.SetActive(false);
        }

        tab.SetActive(true);
    }

    public void OpenWeaponTap()
    {
        OpenTab(Tabs[3]); // 무기 탭은 인덱스 3이라고 가정합니다.
        Inventory.instance.sort.SetActive(true);
        if (Inventory.instance.onsortTap)
        {
            Inventory.instance.OnSortTap();
        }
    }

    public void OpenMaTap()
    {
        OpenTab(Tabs[2]); // 재료 탭은 인덱스 2라고 가정합니다.
        Inventory.instance.sort.SetActive(false);
    }

    public void OpenFoodTap()
    {
        OpenTab(Tabs[1]); // 음식 탭은 인덱스 1이라고 가정합니다.
        Inventory.instance.sort.SetActive(false);
    }

    public void OpenImTap()
    {
        OpenTab(Tabs[0]); // 중요 탭은 인덱스 0이라고 가정합니다.
        Inventory.instance.sort.SetActive(false);
    }

    public void CloseTap()
    {
        UI.SetActive(false);
    }
}

