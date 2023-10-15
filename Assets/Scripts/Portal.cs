using UnityEngine;
using UnityEngine.SceneManagement; // �� ������ ���� ���ӽ����̽� �߰�
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
            SceneManager.LoadScene("Boss_Scene"); // �� �̸����� �� �� �ε�
        }
    }
}
