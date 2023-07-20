using UnityEngine;

public class DialogueButton : ButtonScript
{
    // 대화 내용을 저장할 변수
    public string dialogueText = "대화대화대화대화대화대화";

    public override void Interact()
    {
        base.Interact(); // 부모 클래스의 Interact() 메서드 호출
        Debug.Log("Dialogue Button Interact");
        ShowDialogue();
    }

    void ShowDialogue()
    {
        // 대화 출력
        Debug.Log(dialogueText);
    }
}