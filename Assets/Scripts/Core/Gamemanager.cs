using monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : Singleton<Gamemanager>
{
    Monster monster;
    public Monster Monster => monster;

    protected override void OnInitialize()
    {
        base.OnInitialize();
        monster = FindObjectOfType<Monster>();
    }


}
