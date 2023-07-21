using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    PlayerController player;
    public PlayerController Player => player;

    protected override void OnInitialize()
    {
        base.OnInitialize();
        player = FindObjectOfType<PlayerController>();
    }
}
