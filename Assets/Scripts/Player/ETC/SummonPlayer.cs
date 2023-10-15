using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SummonPlayer : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("TEST");
        PlayerController playerController = FindAnyObjectByType<PlayerController>();
        playerController.gameObject.transform.position = transform.position;
        playerController.enabled = true;
        playerController.StopInputKey(true);
    }
}
