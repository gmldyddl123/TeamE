using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManage : MonoBehaviour
{
    public enum DialogueState
    {
        UnDialogue,
        Dialogue,
    }

    DialogueState dialogueState;

    private void OnTriggerEnter(Collider other)
    {
        if(dialogueState == DialogueState.UnDialogue)
        {

        }
    }
}
