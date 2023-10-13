using UnityEngine;
using UnityEngine.UI; 

public class CloseTap : MonoBehaviour
{
    private Button button;
    public GameObject parent;
    private void Awake()
    {
        button = GetComponent<Button>();
    }
    private void Start()
    {
        button.onClick.AddListener(CloseParent);
    }

    private void CloseParent()
    {
        parent.SetActive(false);
    }
}

