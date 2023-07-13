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

    // ��ư�� ������ ��, ��ȭ ������ �ҷ��� ȭ�鿡 ����մϴ�.
    void OnMouseDown()
    {

        // �ݶ��̴��� ����Ʈ NPC���� Ȯ���մϴ�.
        if (whoCollider.gameObject.tag == "QuestNPC")
        {

            // ��ȭ ������ �ҷ��� ȭ�鿡 ����մϴ�.
            TextAsset dialogueData = Resources.Load<TextAsset>(dialogueFileName);
            Debug.Log(dialogueData.text);

        }
        else if (whoCollider.gameObject.tag == "MerchantNPC")
        {

            // ������ ���ϴ�.
            // TODO: ���� ���� ����

        }
    }
}