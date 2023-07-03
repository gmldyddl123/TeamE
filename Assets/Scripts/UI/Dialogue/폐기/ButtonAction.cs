using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAction : MonoBehaviour
{
    public Button button;
    public DialogueTrigger currentDialogueNpc;

    private void Awake()
    {
        currentDialogueNpc = FindObjectOfType<DialogueTrigger>();
        button = FindObjectOfType<Button>();

        button.onClick.AddListener(StartDialogue);
    }

    void StartDialogue()
    {
        currentDialogueNpc.TriggerDialogue();
    }

}
