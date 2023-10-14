using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmingCheker : MonoBehaviour
{
    PlayerController playerController;
    private void Awake()
    {
        playerController = transform.parent.GetComponent<PlayerController>();
    }


    private void OnTriggerEnter(Collider other)
    {
        playerController.SwimmingBool = true;
    }

    private void OnTriggerExit(Collider other)
    {
        playerController.SwimmingBool = false;
    }

}
