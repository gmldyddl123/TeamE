using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreNPC : MonoBehaviour,IInteractable
{
    public bool IsDirectUse => true;
    public GameObject StoreTap;

    public event Action<IInteractable> OnDestroyed;

    public void Use()
    {
        UseChecker checker = FindObjectOfType<UseChecker>();
        if (checker)
        {
            StoreTap.SetActive(true);
        }
    }
}
