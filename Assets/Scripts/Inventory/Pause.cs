using Cinemachine;
using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public CinemachineInputProvider playerCamera; 
    public PlayerController actions;
    InventoryInputAction inventoryInputAction;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        inventoryInputAction = new InventoryInputAction();
        // �� ���� PlayerCamera ������Ʈ�� ã���ϴ�.
        if (playerCamera == null)
        {
            Debug.LogError("PlayerCamera ������Ʈ�� ���� �������� �ʽ��ϴ�.");
        }
    }
    private void Start()
    {
        
    }
    private void OnEnable()
    {
        Time.timeScale = 0f; // ���� �Ͻ�����
        actions.StopInputKey(false); // �÷��̾� �Է� ��Ȱ��ȭ
        actions.UserUIActive(false);
        inventoryInputAction.Disable(); // �κ��丮 �Է� ��Ȱ��ȭ
        playerCamera.enabled = false; // �÷��̾� ī�޶� ��Ʈ�� ��Ȱ��ȭ
        CursorManager.Instance.OnUIActivated();
    }

    private void OnDisable()
    {
        Time.timeScale = 1f; // ���� �簳
        actions.StopInputKey(true); // �÷��̾� �Է� ��Ȱ��ȭ
        actions.UserUIActive(true);
        inventoryInputAction.Enable(); // �κ��丮 �Է� Ȱ��ȭ
        if (playerCamera != null) // playerCamera�� �����ϴ��� Ȯ��
        {
            playerCamera.enabled = true; // �÷��̾� ī�޶� ��Ʈ�� Ȱ��ȭ
        }
        CursorManager.Instance.OnUIDeactivated();
    }
}
