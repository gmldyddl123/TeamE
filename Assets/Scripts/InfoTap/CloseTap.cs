using UnityEngine;
using UnityEngine.UI; 

public class CloseTap : MonoBehaviour
{
    private Button button;
    public GameObject parent;

    private void Start()
    {
        button = transform.GetChild(0).GetComponent<Button>(); 
        button.onClick.AddListener(CloseParent);
    }

    private void CloseParent()
    {
        parent.SetActive(false);
    }
}

