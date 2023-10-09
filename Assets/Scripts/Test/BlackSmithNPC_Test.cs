using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSmithNPC_Test : MonoBehaviour, IInteractable
{
    public bool IsDirectUse => true;
    public GameObject weponInfo;
    public void Use()
    {
        UseChecker checker = FindObjectOfType<UseChecker>();
        if (checker)
        {
            weponInfo.SetActive(true);
        }
    }
}
