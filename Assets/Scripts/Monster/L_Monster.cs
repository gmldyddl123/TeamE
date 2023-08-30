using player;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace monster
{
   
    public class L_Monster : Monster
    {
        protected override void Awake()
        {
            base.Awake();
            Distance = 5;
        }




    }
}


