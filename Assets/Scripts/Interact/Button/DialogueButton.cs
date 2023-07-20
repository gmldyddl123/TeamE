using UnityEngine;

public class DialogueButton : ButtonScript
{
    // ��ȭ ������ ������ ����
    public string dialogueText = "��ȭ��ȭ��ȭ��ȭ��ȭ��ȭ";

    public override void Interact()
    {
        base.Interact(); // �θ� Ŭ������ Interact() �޼��� ȣ��
        Debug.Log("Dialogue Button Interact");
        ShowDialogue();
    }

    void ShowDialogue()
    {
        // ��ȭ ���
        Debug.Log(dialogueText);
    }
}