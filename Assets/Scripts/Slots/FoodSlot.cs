using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSlot : MonoBehaviour
{
    public Item_FoodItem _item;

    public GameObject foodView;

    public FoodMaker foodMaker;

    private void Awake()
    {
        //foodView = GetComponent<GameObject>();
        //foodMaker = GetComponent<FoodMaker>();
    }
    public void Set()
    {
        foodView.SetActive(true);
        foodMaker.Get(_item);
    }
}