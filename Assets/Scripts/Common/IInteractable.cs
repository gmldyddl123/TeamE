using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IInteractable
{
    bool IsDirectUse
    {
        get;    // ��ȣ�ۿ� ������ ������Ʈ�� ���� ��밡���� ������, ���� ��� ������ ������ ǥ���ϱ� ���� ������Ƽ
    }

    void Use(); // ����ϴ� ����� �ִٰ� ������ ���� ��
}
