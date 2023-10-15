using UnityEngine;
using UnityEngine.SceneManagement; // �� ������ ���� ���ӽ����̽� �߰�
using System;
using player;

public class Portal : MonoBehaviour, IInteractable
{
    public bool IsDirectUse => true;

    public event Action<IInteractable> OnDestroyed;

    public void Use()
    {
        UseChecker checker = FindObjectOfType<UseChecker>();
        if (checker)
        {
            PlayerController playerController = FindAnyObjectByType<PlayerController>();
            playerController.StopInputKey(false);
            playerController.PlayerEnterIdleState();
            playerController.enabled = false;

            SceneManager.LoadScene("Boss_Scene"); // �� �̸����� �� �� �ε�
        }
    }
}
