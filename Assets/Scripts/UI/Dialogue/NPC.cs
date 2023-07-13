using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    // Button ��ũ��Ʈ

    public int id;
    public BoxCollider boxCollider;
    public GameObject interactUI;
    public Button btn;
    public CanvasGroup btnCanvasGroup;

    void Start()
    {
        interactUI = GameObject.FindGameObjectWithTag("InteractUI");
        btn = interactUI.GetComponentInChildren<Button>();
        btnCanvasGroup = interactUI.GetComponentInChildren<CanvasGroup>();
        // �ݶ��̴��� ����մϴ�.
        boxCollider = GetComponent<BoxCollider>();

        btnCanvasGroup.alpha = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        // �÷��̾� �±װ� �����Ǿ����� Ȯ���մϴ�.
        if (other.gameObject.tag == "Player")
        {
            // ��ư�� Ȱ��ȭ�մϴ�.
            btnCanvasGroup.alpha = 1;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // �÷��̾� �±װ� �������� �ʾҴ��� Ȯ���մϴ�.
        if (other.gameObject.tag != "Player")
        {
            // ��ư�� ��Ȱ��ȭ�մϴ�.
            btnCanvasGroup.alpha = 0;
        }
    }
}
