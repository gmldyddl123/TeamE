using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리를 위한 네임스페이스 추가
using System;

public class Portal : MonoBehaviour, IInteractable
{
    public bool IsDirectUse => true;

    public event Action<IInteractable> OnDestroyed;

    public void Use()
    {
        UseChecker checker = FindObjectOfType<UseChecker>();
        if (checker)
        {
            SceneManager.LoadScene("Boss_Scene"); // 씬 이름으로 새 씬 로드
        }
    }
}
