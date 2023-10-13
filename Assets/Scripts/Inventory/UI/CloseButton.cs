using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    private Button button;
    public GameObject parent;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(CloseParent);
    }

    private void CloseParent()
    {
        parent.SetActive(false);
    }
}
