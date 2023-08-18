using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class UseChecker : MonoBehaviour
{
    public Action<IInteractable> onItemUse;

    private void OnTriggerEnter(Collider other)
    {
        // üĿ�� Ʈ���� ������ �ٸ� �ö��̴��� ������ ��
        Transform target = other.transform;
        IInteractable obj = null;
        do
        {
            obj = target.GetComponent<IInteractable>(); // IInteractable �������� �õ�
            target = target.parent;                     // target�� �θ�� ����
        } while (obj == null && target != null);        // obj�� ã�ų� ���̻� �θ� ������ ���� ����

        if (obj != null)
        {
            onItemUse?.Invoke(obj);     // IInteractable�� ��ӹ��� ������Ʈ�� ������ ����
        }
    }
}