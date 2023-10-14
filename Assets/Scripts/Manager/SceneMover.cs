using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMover : MonoBehaviour
{
    PlayerController player;
    public GameObject genPos;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    public void BossSceneEnter()
    {
        player.transform.position = genPos.transform.position;
        SceneManager.LoadScene("Boss_Scene");
    }
}
