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

    // 상호작용 가능한 버튼 배열
    public Button[] selectButtons; // 선택 버튼들을 담을 배열
    private int currentSelectedButtonIndex = 0;
    private int previousSelectedButtonIndex = 0;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
        inputActions = new PlayerInputAction();
        // Awake에서 선택 버튼들을 자동으로 찾아 배열에 할당
        FindSelectButtons();
    }

    private void FindSelectButtons()
    {
        // 씬에 있는 모든 버튼을 찾아서 배열에 할당
        Button[] allButtons = GameObject.FindObjectsOfType<Button>();

        // 선택 버튼만을 골라서 배열에 할당
        List<Button> tempSelectButtons = new List<Button>();
        foreach (Button button in allButtons)
        {
            if (button.GetComponent<ButtonScript>() != null) // 버튼에 ButtonScript 컴포넌트가 있으면 선택 버튼으로 간주
            {
                tempSelectButtons.Add(button);
            }
        }

        // 배열에 담긴 선택 버튼들을 selectButtons 배열로 옮깁니다.
        selectButtons = tempSelectButtons.ToArray();
    }

    private void OnEnable()
    {
        // UI 액션맵 활성화
        inputActions.UI.Enable();

        // UI 액션맵의 이벤트 핸들러 연결
        inputActions.UI.Interact.performed += HandleInteractInput;
        inputActions.UI.Escape.performed += HandleEscapeInput;
        inputActions.UI.Select.performed += HandleSelectInput; // 마우스 휠 이벤트 핸들러 연결
    }

    private void OnDisable()
    {
        // UI 액션맵의 이벤트 핸들러 해제
        inputActions.UI.Select.performed -= HandleSelectInput; // 마우스 휠 이벤트 핸들러 해제
        inputActions.UI.Escape.performed -= HandleEscapeInput;
        inputActions.UI.Interact.performed -= HandleInteractInput;

        // UI 액션맵 비활성화
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
            // 마우스 휠을 위로 스크롤하면 다음 버튼 강조
            previousSelectedButtonIndex = currentSelectedButtonIndex;
            currentSelectedButtonIndex = (currentSelectedButtonIndex + 1) % selectButtons.Length;
        }
        else if (scrollValue < 0)
        {
            // 마우스 휠을 아래로 스크롤하면 이전 버튼 강조
            previousSelectedButtonIndex = currentSelectedButtonIndex;
            currentSelectedButtonIndex = (currentSelectedButtonIndex - 1 + selectButtons.Length) % selectButtons.Length;
        }

        // 선택된 버튼 강조 표시
        selectButtons[currentSelectedButtonIndex].GetComponent<ButtonScript>().HighlightButton(true);
        selectButtons[previousSelectedButtonIndex].GetComponent<ButtonScript>().HighlightButton(false);
    }
}