using monster;
using System;
using UnityEngine;

public class MonsterEvent : MonoBehaviour
{
  
    public event Action<Monster> OnMonsterAttacked;


    public void MonsterAttacked(Monster attackedMonster)
    {
        OnMonsterAttacked?.Invoke(attackedMonster);
    }
}