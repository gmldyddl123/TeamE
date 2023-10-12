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
        Time.timeScale = 0f; // ���� �Ͻ�����
        actions.Disable(); // �÷��̾� �Է� ��Ȱ��ȭ
        playerCamera.enabled = false; // �÷��̾� ī�޶� ��Ʈ�� ��Ȱ��ȭ
    }

    private void OnDisable()
    {
        Time.timeScale = 1f; // ���� �簳
        actions.Enable(); // �÷��̾� �Է� Ȱ��ȭ
        playerCamera.enabled = true; // �÷��̾� ī�޶� ��Ʈ�� Ȱ��ȭ
    }
}
