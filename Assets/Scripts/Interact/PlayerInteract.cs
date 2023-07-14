using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    PlayerInputSystem player;

    private void Awake()
    {
        player = GetComponent<PlayerInputSystem>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            float interactRange = 2.0f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach(Collider collider in colliderArray)
            {
                if(collider.TryGetComponent(out NPCInteractable npcInteractable))
                {
                    npcInteractable.Interact(transform);
                    player.IsInteract = true;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            player.IsInteract = false;
        }
    }
}
