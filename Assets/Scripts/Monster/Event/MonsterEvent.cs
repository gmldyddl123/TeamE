using monster;
using System;
using UnityEngine;

public class MonsterEvent : MonoBehaviour
{
  
    public Action<Monster> OnMonsterAttacked;
    public Action SpawnCountChange;


    public void MonsterAttacked(Monster attackedMonster)
    {
        OnMonsterAttacked?.Invoke(attackedMonster);
    }
}