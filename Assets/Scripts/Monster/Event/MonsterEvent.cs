using monster;
using System;
using UnityEngine;

public class MonsterEvent : MonoBehaviour
{
  
    public Action<Monster_Base> OnMonsterAttacked;
    public Action SpawnCountChange;
    public Action OnItemDrop;
    public Action<int> PlusQuestCount;
    

    public Func<int, bool> test;
    private void Start()
    {
        test += TEST1;
    }

    public void MonsterAttacked(Monster_Base attackedMonster)
    {
        OnMonsterAttacked?.Invoke(attackedMonster);
    }
   

    bool TEST1(int test)
    {
        return true;
    }
}