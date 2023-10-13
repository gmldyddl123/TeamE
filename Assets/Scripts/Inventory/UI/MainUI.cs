using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public GameObject UI;
    public List<GameObject> Tabs; // ��� ���� �����ϴ� ����Ʈ
    private List<Button> buttons; // ��� ��ư�� �����ϴ� ����Ʈ

    private void Awake()
    {
        buttons = new List<Button>();

        // ��� �ڽ� ������Ʈ���� ��ư ������Ʈ�� �����ɴϴ�.
        foreach (Transform child in transform)
        {
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                buttons.Add(button);
            }
        }

        // ��ư�� ������ �߰�
        if (buttons.Count >= 5)
        {
            buttons[0].onClick.AddListener(CloseTap); // Close ��ư
            buttons[1].onClick.AddListener(OpenImTap); // Important �� ��ư
            buttons[2].onClick.AddListener(OpenFoodTap); // Food �� ��ư
            buttons[3].onClick.AddListener(OpenMaTap); // Material �� ��ư
            buttons[4].onClick.AddListener(OpenWeaponTap); // Weapon �� ��ư
        }
    }

    // ��� ���� ��Ȱ��ȭ�� �� ������ �Ǹ� Ȱ��ȭ�ϴ� �޼���
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
        OpenTab(Tabs[3]); // ���� ���� �ε��� 3�̶�� �����մϴ�.
        Inventory.instance.sort.SetActive(true);
        if (Inventory.instance.onsortTap)
        {
            Inventory.instance.OnSortTap();
        }
    }

    public void OpenMaTap()
    {
        OpenTab(Tabs[2]); // ��� ���� �ε��� 2��� �����մϴ�.
        Inventory.instance.sort.SetActive(false);
    }

    public void OpenFoodTap()
    {
        OpenTab(Tabs[1]); // ���� ���� �ε��� 1�̶�� �����մϴ�.
        Inventory.instance.sort.SetActive(false);
    }

    public void OpenImTap()
    {
        OpenTab(Tabs[0]); // �߿� ���� �ε��� 0�̶�� �����մϴ�.
        Inventory.instance.sort.SetActive(false);
    }

    public void CloseTap()
    {
        UI.SetActive(false);
    }
}

