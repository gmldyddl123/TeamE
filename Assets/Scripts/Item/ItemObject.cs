using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData item;
    public bool IsDirectUse => true;
    public Action onItemDrop;

    public void Use()
    {
        Inventory.instance.Add(item);
        
        // Notify the UseChecker that this item is being destroyed
        UseChecker checker = FindObjectOfType<UseChecker>();
        if (checker)
        {
            checker.ItemDestroyed(this);
        }

        Destroy(gameObject);
    }
}