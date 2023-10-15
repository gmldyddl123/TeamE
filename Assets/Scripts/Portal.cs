using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리를 위한 네임스페이스 추가
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

            SceneManager.LoadScene("Boss_Scene"); // 씬 이름으로 새 씬 로드
        }
    }
}
