using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemUnMaterialType
{
    Ore,
    Artifact_EXP
}
[CreateAssetMenu(menuName = "Inventory/Item_UnMaterial", fileName = "Item_UnMaterial", order = 2)]
public class Item_UnMaterial : ItemData
{
    public ItemUnMaterialType unMaterialType;
    public float value;
    public int count;
}
