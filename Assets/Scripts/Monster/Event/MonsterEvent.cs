using monster;
using System;
using UnityEngine;

public class MonsterEvent : MonoBehaviour
{
  
    public Action<Monster> OnMonsterAttacked;
    public Action SpawnCountChange;

    public Func<int, bool> test;
    private void Start()
    {
        test += TEST1;
    }

    public void MonsterAttacked(Monster attackedMonster)
    {
        OnMonsterAttacked?.Invoke(attackedMonster);
    }

    bool TEST1(int test)
    {
        return true;
    }
}