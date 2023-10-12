using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cook : MonoBehaviour, IInteractable
{
    public bool IsDirectUse => true;
    public GameObject FoodTap;

    public event Action<IInteractable> OnDestroyed;

    public void Use()
    {
        UseChecker checker = FindObjectOfType<UseChecker>();
        if (checker)
        {
            FoodTap.SetActive(true);
        }
    }
}