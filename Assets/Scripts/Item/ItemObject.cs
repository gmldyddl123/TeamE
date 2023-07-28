using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData item;

    public bool IsDirectUse => true;

    public void Use()
    {
        Inventory.instance.Add(item);
        Destroy(gameObject);
    }
}
