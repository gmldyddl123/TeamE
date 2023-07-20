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
        // ��ư�� ���� ǥ�ø� �����ϴ� ������ �����ϼ���.
        GetComponent<Button>().interactable = !highlight;
        // ��ư �ؽ�Ʈ, �̹��� ���� ���� ȿ���� �ֱ� ���� ������ �� �ֽ��ϴ�.
    }
}