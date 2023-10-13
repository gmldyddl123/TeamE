using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    // Ȱ��ȭ�� UI�� ���� �����մϴ�.
    private int activeUICount = 0;

    // �̱��� �ν��Ͻ�
    public static CursorManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        // ���� ���� �� Ŀ���� ����� ��޴ϴ�.
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // UI�� Ȱ��ȭ�� �� ȣ��Ǵ� �޼���
    public void OnUIActivated()
    {
        activeUICount++;
        UpdateCursorVisibility();
    }

    // UI�� ��Ȱ��ȭ�� �� ȣ��Ǵ� �޼���
    public void OnUIDeactivated()
    {
        activeUICount--;
        UpdateCursorVisibility();
    }

    // Ŀ���� ���ü��� ������Ʈ�մϴ�.
    private void UpdateCursorVisibility()
    {
        bool showCursor = activeUICount > 0;
        Cursor.visible = showCursor;
        Cursor.lockState = showCursor ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
