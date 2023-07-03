using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    
    public Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialoguePanel.SetActive(true);
        //Debug.Log("Starting" + dialogue.name);
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        //Debug.Log(sentence);
        dialogueText.text = sentence;
    }

    private void EndDialogue()
    {
        Debug.Log("End");
        dialoguePanel.SetActive(false);
    }
}
