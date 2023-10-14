using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmingCheker : MonoBehaviour
{
    PlayerController playerController;

    int Water_Layer_Name;

    private void Awake()
    {
        playerController = transform.parent.GetComponent<PlayerController>();
        Water_Layer_Name = LayerMask.NameToLayer("Water");
    }


    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(!playerController.WaterDive && other.gameObject.layer == Water_Layer_Name)
        {
        
            playerController.SwimmingBool = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!playerController.WaterDive && other.gameObject.layer == Water_Layer_Name)
        {
        
            playerController.SwimmingBool = false;
        }
    }

}
