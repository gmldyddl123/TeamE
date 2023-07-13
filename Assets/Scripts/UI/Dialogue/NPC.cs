using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    // Button 스크립트

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
        // 콜라이더를 등록합니다.
        boxCollider = GetComponent<BoxCollider>();

        btnCanvasGroup.alpha = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        // 플레이어 태그가 감지되었는지 확인합니다.
        if (other.gameObject.tag == "Player")
        {
            // 버튼을 활성화합니다.
            btnCanvasGroup.alpha = 1;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // 플레이어 태그가 감지되지 않았는지 확인합니다.
        if (other.gameObject.tag != "Player")
        {
            // 버튼을 비활성화합니다.
            btnCanvasGroup.alpha = 0;
        }
    }
}
