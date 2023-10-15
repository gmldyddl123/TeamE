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
        // �� ���� PlayerCamera ������Ʈ�� ã���ϴ�.
        playerCamera = FindObjectOfType<CinemachineInputProvider>(); 
        if (playerCamera == null)
        {
            Debug.LogError("PlayerCamera ������Ʈ�� ���� �������� �ʽ��ϴ�.");
        }
    }


    private void OnEnable()
    {
        Time.timeScale = 0f; // ���� �Ͻ�����
        actions.Disable(); // �÷��̾� �Է� ��Ȱ��ȭ
        inventoryInputAction.Disable(); // �κ��丮 �Է� ��Ȱ��ȭ
        playerCamera.enabled = false; // �÷��̾� ī�޶� ��Ʈ�� ��Ȱ��ȭ
        CursorManager.Instance.OnUIActivated();
    }

    private void OnDisable()
    {
        Time.timeScale = 1f; // ���� �簳
        actions.Enable(); // �÷��̾� �Է� Ȱ��ȭ
        inventoryInputAction.Enable(); // �κ��丮 �Է� Ȱ��ȭ
        if (playerCamera != null) // playerCamera�� �����ϴ��� Ȯ��
        {
            playerCamera.enabled = true; // �÷��̾� ī�޶� ��Ʈ�� Ȱ��ȭ
        }
        CursorManager.Instance.OnUIDeactivated();
    }
}
