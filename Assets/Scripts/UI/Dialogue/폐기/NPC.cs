using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject interactButton;

    private void OnTriggerEnter(Collider other)
    {
        interactButton.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        interactButton.SetActive(false);
    }
}
