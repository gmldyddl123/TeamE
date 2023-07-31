using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 착용한 부위
/// </summary>
public enum Wearingarea
{
    bandana,
    flower,
    goblet,
    watch,
    Feather
}
[CreateAssetMenu(menuName = "Inventory/Artifact", fileName = "Artifact", order = 6)]
public class Item_Artifact : ItemData
{
    [Header("착용 가능한 부위")]
    public Wearingarea water;
    
}
