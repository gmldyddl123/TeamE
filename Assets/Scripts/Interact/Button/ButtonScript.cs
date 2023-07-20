using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public virtual void Interact()
    {
        Debug.Log("Button Interact");
    }


    public virtual void HighlightButton(bool highlight)
    {
        // 버튼의 강조 표시를 설정하는 로직을 구현하세요.
        GetComponent<Button>().interactable = !highlight;
        // 버튼 텍스트, 이미지 등을 강조 효과를 주기 위해 조정할 수 있습니다.
    }
}