using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class UseChecker : MonoBehaviour
{
    public Action<IInteractable> onItemUse;
    private List<IInteractable> interactablesInRange = new List<IInteractable>();

    private void OnTriggerEnter(Collider other)
    {
        Transform target = other.transform;
        IInteractable obj = null;
        do
        {
            obj = target.GetComponent<IInteractable>();
            target = target.parent;
        } while (obj == null && target != null);

        if (obj != null)
        {
            interactablesInRange.Add(obj);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInteractable obj = other.GetComponent<IInteractable>();
        if (obj != null)
        {
            interactablesInRange.Remove(obj);
        }
    }

    public IInteractable GetClosestInteractable(Vector3 position)
    {
        IInteractable closestItem = null;
        float closestDistance = float.MaxValue;

        foreach (var item in interactablesInRange)
        {
            float distance = Vector3.Distance(position, ((MonoBehaviour)item).transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestItem = item;
            }
        }

        return closestItem;
    }
    public void ItemDestroyed(IInteractable item)
    {
        interactablesInRange.Remove(item);
    }
}
