using monster;
using System;
using UnityEngine;

public class MonsterEvent : MonoBehaviour
{
      
    /// <summary>
    /// 몬스터가 공격당함을 알리기 위한 델리게이트 
    /// </summary>
    public Action<Monster_Base> OnMonsterAttacked;
    public Action SpawnCountChange;
    public Action OnItemDrop;
    public Action<int> PlusQuestCount;
    

    /// <summary>
    /// 몬스터가 공격 당했는지 알리는 이벤트 함수
    /// </summary>
    /// <param name="attackedMonster">공격당한 몬스터</param>
    public void MonsterAttacked(Monster_Base attackedMonster)
    {
        OnMonsterAttacked?.Invoke(attackedMonster);
    }
   

  
}