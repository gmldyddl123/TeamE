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
    public Transform playerTransform; 

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
        inputActions.Player.Interactable.performed -= OnInteractablePerformed;
        inputActions.Player.Interactable.canceled -= OnInteractableCanceled;
        inputActions.Player.Disable();
    }


    private void Start()
    {
        interactionCollider.enabled = true; 
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform; 
        }
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
        Debug.Log("Interactable performed"); // 이 로그가 두 번 출력되는지 확인
        UseClosestInteractable();
    }

    private void OnInteractableCanceled(InputAction.CallbackContext context)
    {
        
    }

    private void UseClosestInteractable()
    {
        var closestInteractable = GetClosestInteractable(playerTransform.position);

        if (closestInteractable != null)
        {
            closestInteractable.Use();
            onItemUse?.Invoke(closestInteractable);

            // 한 번 사용된 후, 리스트에서 제거하여 다른 상호작용 가능한 객체의 사용을 방지합니다.
            RemoveInteractable(closestInteractable);
        }
    }



    private IInteractable GetClosestInteractable(Vector3 position)
    {
        IInteractable closestItem = null;
        float closestDistance = float.MaxValue;

        foreach (var item in interactablesInRange)
        {
            float distance = Vector3.Distance(playerTransform.position, ((MonoBehaviour)item).transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestItem = item;
            }
        }

        return closestItem;
    }
}