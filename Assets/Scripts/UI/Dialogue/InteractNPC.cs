using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractNPC : MonoBehaviour
{
    PlayerInputSystem player;
    public Button interactButton;

    private void Awake()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        player = other.GetComponent<PlayerInputSystem>();
        if(player != null)
        {
            interactButton.enabled = true;
        }
    }
}
