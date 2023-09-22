using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Base : PooledObject
{

    public bool isFriendsAttacked;
    public Action<float> onHealthChange { get; set; }

    public virtual void Detect()
    {
       
    }
}
