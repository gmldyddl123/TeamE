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


