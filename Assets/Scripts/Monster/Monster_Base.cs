using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Base : PooledObject
{
    /// <summary>
    /// �ֺ� ���Ͱ� ���ݹ޾Ѵ����� ���� ���θ� ���� boolŸ��( true�� �� �ֺ� ���Ͱ� ���ݹ���)
    /// </summary>
    public bool isFriendsAttacked;

    /// <summary>
    /// ������ HP��ȭ�� �˸��� ��������Ʈ UI ������
    /// </summary>
    public Action<float> onHealthChange { get; set; }

    /// <summary>
    /// ���Ͱ� �÷��̾ ���������� Detect_State ���Կ� �Լ�
    /// </summary>
    public virtual void Detect()
    {
       
    }
}
