using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public PlayerCamera playerCamera;
    PlayerInputAction actions;
    private void Awake()
    {
        actions = new PlayerInputAction();
    }

    private void OnEnable()
    {
        Time.timeScale = 0f; // 게임 일시정지
        actions.Disable(); // 플레이어 입력 비활성화
        playerCamera.enabled = false; // 플레이어 카메라 컨트롤 비활성화
    }

    private void OnDisable()
    {
        Time.timeScale = 1f; // 게임 재개
        actions.Enable(); // 플레이어 입력 활성화
        playerCamera.enabled = true; // 플레이어 카메라 컨트롤 활성화
    }
}
