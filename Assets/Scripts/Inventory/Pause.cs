using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private CinemachineInputProvider playerCamera; 
    PlayerInputAction actions;
    InventoryInputAction inventoryInputAction;
    private void Awake()
    {
        actions = new PlayerInputAction();
        inventoryInputAction = new InventoryInputAction();
        // 씬 내의 PlayerCamera 컴포넌트를 찾습니다.
        playerCamera = FindObjectOfType<CinemachineInputProvider>(); 
        if (playerCamera == null)
        {
            Debug.LogError("PlayerCamera 컴포넌트가 씬에 존재하지 않습니다.");
        }
    }


    private void OnEnable()
    {
        Time.timeScale = 0f; // 게임 일시정지
        actions.Disable(); // 플레이어 입력 비활성화
        inventoryInputAction.Disable(); // 인벤토리 입력 비활성화
        playerCamera.enabled = false; // 플레이어 카메라 컨트롤 비활성화
        CursorManager.Instance.OnUIActivated();
    }

    private void OnDisable()
    {
        Time.timeScale = 1f; // 게임 재개
        actions.Enable(); // 플레이어 입력 활성화
        inventoryInputAction.Enable(); // 인벤토리 입력 활성화
        if (playerCamera != null) // playerCamera가 존재하는지 확인
        {
            playerCamera.enabled = true; // 플레이어 카메라 컨트롤 활성화
        }
        CursorManager.Instance.OnUIDeactivated();
    }
}
