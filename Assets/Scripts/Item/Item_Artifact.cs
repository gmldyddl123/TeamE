using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ����
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
    [Header("���� ������ ����")]
    public Wearingarea water;
    
}
