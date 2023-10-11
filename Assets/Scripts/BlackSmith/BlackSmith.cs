using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSmith : MonoBehaviour,IInteractable
{
    public bool IsDirectUse => true;
    public GameObject weponUpInfo;

    public event Action<IInteractable> OnDestroyed;

    public void Use()
    {
        UseChecker checker = FindObjectOfType<UseChecker>();
        if (checker)
        {
            weponUpInfo.SetActive(true);
        }
    }
}