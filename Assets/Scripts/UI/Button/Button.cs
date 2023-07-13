using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    public string dialogueFileName;
    public Collider whoCollider;

    // Use this for initialization
    void Start()
    {

    }

    // 버튼이 눌렸을 때, 대화 파일을 불러와 화면에 출력합니다.
    void OnMouseDown()
    {

        // 콜라이더가 퀘스트 NPC인지 확인합니다.
        if (whoCollider.gameObject.tag == "QuestNPC")
        {

            // 대화 파일을 불러와 화면에 출력합니다.
            TextAsset dialogueData = Resources.Load<TextAsset>(dialogueFileName);
            Debug.Log(dialogueData.text);

        }
        else if (whoCollider.gameObject.tag == "MerchantNPC")
        {

            // 상점을 엽니다.
            // TODO: 상점 로직 구현

        }
    }
}