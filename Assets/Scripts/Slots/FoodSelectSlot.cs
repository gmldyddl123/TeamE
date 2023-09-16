using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FoodSelectSlot : MonoBehaviour
{
    public FoodInfo info;
    public ItemData item;
    public FoodMaterialList materialList;
    public void Set()
    {
        info.Get(item);
        materialList.Get(item);
    }
}