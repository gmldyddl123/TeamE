using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemMaterialType
{
    foodMaterial,
    Player_EXP
}
[CreateAssetMenu(menuName = "Inventory/Item_Materia", fileName = "Item_Materia", order = 1)]
public class Item_Material : ItemData
{
    public ItemMaterialType MaterialType;
    public float value;
}
