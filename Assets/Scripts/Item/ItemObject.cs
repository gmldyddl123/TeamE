using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    public bool IsDirectUse => true;
    public Action onItemDrop;

    public void Use()
    {
        onItemDrop?.Invoke();
        Destroy(gameObject);
    }
}

