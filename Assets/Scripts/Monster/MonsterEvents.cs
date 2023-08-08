using monster;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEvents : MonoBehaviour
{
    // ���Ͱ� ������ ���� �� �߻��ϴ� �̺�Ʈ
    public event Action<Monster> OnMonsterAttacked;

    // ������ ���� ���͸� �����ϸ� �̺�Ʈ�� �߻���Ű�� �޼���
    public void MonsterAttacked(Monster attackedMonster)
    {
        OnMonsterAttacked?.Invoke(attackedMonster);
    }
}