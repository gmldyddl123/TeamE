using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemUnMaterialType
{
    Ore,
    Artifact_EXP,
    Player_EXP
}
[CreateAssetMenu(menuName = "Inventory/Item_UpMaterial", fileName = "Item_UpMaterial", order = 3)]
public class Item_UpMaterial : ItemData
{
    public ItemUnMaterialType unMaterialType;
    public float plusEXP;
}
