using monster;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEvents : MonoBehaviour
{
    // 몬스터가 공격을 받을 때 발생하는 이벤트
    public event Action<Monster> OnMonsterAttacked;

    // 공격을 받은 몬스터를 전달하면 이벤트를 발생시키는 메서드
    public void MonsterAttacked(Monster attackedMonster)
    {
        OnMonsterAttacked?.Invoke(attackedMonster);
    }
}