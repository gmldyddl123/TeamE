using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    PlayerController player;
    PlayerInputAction inputActions;

    // ��ȣ�ۿ� ������ ��ư �迭
    public Button[] selectButtons; // ���� ��ư���� ���� �迭
    private int currentSelectedButtonIndex = 0;
    private int previousSelectedButtonIndex = 0;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
        inputActions = new PlayerInputAction();
        // Awake���� ���� ��ư���� �ڵ����� ã�� �迭�� �Ҵ�
        FindSelectButtons();
    }

    private void FindSelectButtons()
    {
        // ���� �ִ� ��� ��ư�� ã�Ƽ� �迭�� �Ҵ�
        Button[] allButtons = GameObject.FindObjectsOfType<Button>();

        // ���� ��ư���� ��� �迭�� �Ҵ�
        List<Button> tempSelectButtons = new List<Button>();
        foreach (Button button in allButtons)
        {
            if (button.GetComponent<ButtonScript>() != null) // ��ư�� ButtonScript ������Ʈ�� ������ ���� ��ư���� ����
            {
                tempSelectButtons.Add(button);
            }
        }

        // �迭�� ��� ���� ��ư���� selectButtons �迭�� �ű�ϴ�.
        selectButtons = tempSelectButtons.ToArray();
    }

    private void OnEnable()
    {
        // UI �׼Ǹ� Ȱ��ȭ
        inputActions.UI.Enable();

        // UI �׼Ǹ��� �̺�Ʈ �ڵ鷯 ����
        inputActions.UI.Interact.performed += HandleInteractInput;
        inputActions.UI.Escape.performed += HandleEscapeInput;
        inputActions.UI.Select.performed += HandleSelectInput; // ���콺 �� �̺�Ʈ �ڵ鷯 ����
    }

    private void OnDisable()
    {
        // UI �׼Ǹ��� �̺�Ʈ �ڵ鷯 ����
        inputActions.UI.Select.performed -= HandleSelectInput; // ���콺 �� �̺�Ʈ �ڵ鷯 ����
        inputActions.UI.Escape.performed -= HandleEscapeInput;
        inputActions.UI.Interact.performed -= HandleInteractInput;

        // UI �׼Ǹ� ��Ȱ��ȭ
        inputActions.UI.Disable();
    }

    private void HandleInteractInput(InputAction.CallbackContext context)
    {
        Button currentSelectedButton = selectButtons[currentSelectedButtonIndex];
        Debug.Log(currentSelectedButton.name);

        ButtonScript buttonScript = currentSelectedButton.GetComponent<ButtonScript>();
        if (buttonScript != null)
        {
            buttonScript.Interact();
        }
    }

    private void HandleEscapeInput(InputAction.CallbackContext context)
    {
        player.IsInteract = false;
    }

    private void HandleSelectInput(InputAction.CallbackContext context)
    {
        float scrollValue = context.ReadValue<float>();
        if (scrollValue > 0)
        {
            // ���콺 ���� ���� ��ũ���ϸ� ���� ��ư ����
            previousSelectedButtonIndex = currentSelectedButtonIndex;
            currentSelectedButtonIndex = (currentSelectedButtonIndex + 1) % selectButtons.Length;
        }
        else if (scrollValue < 0)
        {
            // ���콺 ���� �Ʒ��� ��ũ���ϸ� ���� ��ư ����
            previousSelectedButtonIndex = currentSelectedButtonIndex;
            currentSelectedButtonIndex = (currentSelectedButtonIndex - 1 + selectButtons.Length) % selectButtons.Length;
        }

        // ���õ� ��ư ���� ǥ��
        selectButtons[currentSelectedButtonIndex].GetComponent<ButtonScript>().HighlightButton(true);
        selectButtons[previousSelectedButtonIndex].GetComponent<ButtonScript>().HighlightButton(false);
    }
}