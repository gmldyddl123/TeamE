using UnityEngine;

public class ShopButton : ButtonScript
{
    public override void Interact()
    {
        base.Interact(); // �θ� Ŭ������ Interact() �޼��� ȣ��
        Debug.Log("Shop Button Interact");
        // ���� �̿� ��� ����
    }
}