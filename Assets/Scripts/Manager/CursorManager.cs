using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    // 활성화된 UI의 수를 추적합니다.
    private int activeUICount = 0;

    // 싱글톤 인스턴스
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
        // 게임 시작 시 커서를 숨기고 잠급니다.
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // UI가 활성화될 때 호출되는 메서드
    public void OnUIActivated()
    {
        activeUICount++;
        UpdateCursorVisibility();
    }

    // UI가 비활성화될 때 호출되는 메서드
    public void OnUIDeactivated()
    {
        activeUICount--;
        UpdateCursorVisibility();
    }

    // 커서의 가시성을 업데이트합니다.
    private void UpdateCursorVisibility()
    {
        bool showCursor = activeUICount > 0;
        Cursor.visible = showCursor;
        Cursor.lockState = showCursor ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
