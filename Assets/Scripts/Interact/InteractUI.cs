using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractUI : MonoBehaviour
{
    [SerializeField] private InteractUI interactUI;

    private void Show()
    {
        interactUI.gameObject.SetActive(true);
    }
    private void Hide()
    {
        interactUI.gameObject.SetActive(false);
    }
}
