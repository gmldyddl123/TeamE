using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IInteractable
{
    event Action<IInteractable> OnDestroyed;
    bool IsDirectUse
    {
        get;    // ��ȣ�ۿ� ������ ������Ʈ�� ���� ��밡���� ������, ���� ��� ������ ������ ǥ���ϱ� ���� ������Ƽ
    }
    
    void Use(); // ����ϴ� ����� �ִٰ� ������ ���� ��
}
