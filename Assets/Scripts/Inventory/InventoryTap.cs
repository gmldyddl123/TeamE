using UnityEngine;

public class InventoryTap : MonoBehaviour
{
    public PlayerCamera playerCamera;
    PlayerInputAction actions;

    GameObject exUI;
    GameObject eqUI;
    GameObject imUI;

    private void Awake()
    {
        actions = new PlayerInputAction(); 

        exUI = transform.GetChild(0).gameObject;
        eqUI = transform.GetChild(1).gameObject;
        imUI = transform.GetChild(2).gameObject;
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

    public void OpenExslotUI()
    {
        exUI.SetActive(true);
        eqUI.SetActive(false);
        imUI.SetActive(false);
    }

    public void OpenEqslotUI()
    {
        exUI.SetActive(false);
        eqUI.SetActive(true);
        imUI.SetActive(false);
    }

    public void OpenImslotUI()
    {
        exUI.SetActive(false);
        eqUI.SetActive(false);
        imUI.SetActive(true);
    }
}


