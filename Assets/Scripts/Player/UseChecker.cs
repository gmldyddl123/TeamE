using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UseChecker : MonoBehaviour
{
    public event Action<IInteractable> onItemUse;
    private List<IInteractable> interactablesInRange = new List<IInteractable>();

    private PlayerInputAction inputActions;
    private Collider interactionCollider;

    private void Awake()
    {
        inputActions = new();
        interactionCollider = GetComponent<Collider>();

        if (interactionCollider == null)
        {
            Debug.LogError($"{nameof(interactionCollider)} is not set on {gameObject.name}!");
        }
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Interactable.performed += OnInteractablePerformed;
        inputActions.Player.Interactable.canceled += OnInteractableCanceled;
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.Interactable.performed -= OnInteractablePerformed;
        inputActions.Player.Interactable.canceled -= OnInteractableCanceled;
    }

    private void Start()
    {
        interactionCollider.enabled = true; // 항상 콜라이더를 활성화합니다.
    }
    private void OnTriggerEnter(Collider other)
    {
        AddInteractable(other.GetComponent<IInteractable>());
    }

    private void OnTriggerExit(Collider other)
    {
        RemoveInteractable(other.GetComponent<IInteractable>());
    }

    private void AddInteractable(IInteractable interactable)
    {
        if (interactable != null && !interactablesInRange.Contains(interactable))
        {
            interactablesInRange.Add(interactable);
            interactable.OnDestroyed += Interactable_OnDestroyed;
        }
    }

    private void RemoveInteractable(IInteractable interactable)
    {
        if (interactable != null)
        {
            interactablesInRange.Remove(interactable);
            interactable.OnDestroyed -= Interactable_OnDestroyed;
        }
    }

    private void Interactable_OnDestroyed(IInteractable interactable)
    {
        RemoveInteractable(interactable);
    }

    private void OnInteractablePerformed(InputAction.CallbackContext context)
    {
        UseClosestInteractable(); // 바로 가장 가까운 상호작용 가능한 객체를 사용합니다.
    }

    private void OnInteractableCanceled(InputAction.CallbackContext context)
    {
    }

    private void UseClosestInteractable()
    {
        var closestInteractable = GetClosestInteractable(transform.position);

        if (closestInteractable != null)
        {
            closestInteractable.Use();
            onItemUse?.Invoke(closestInteractable);

            RemoveInteractable(closestInteractable);
        }
    }


    private IInteractable GetClosestInteractable(Vector3 position)
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
}