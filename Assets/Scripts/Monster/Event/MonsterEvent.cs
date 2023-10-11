using monster;
using System;
using UnityEngine;

public class MonsterEvent : MonoBehaviour
{
      
    /// <summary>
    /// ���Ͱ� ���ݴ����� �˸��� ���� ��������Ʈ 
    /// </summary>
    public Action<Monster_Base> OnMonsterAttacked;
    public Action SpawnCountChange;
    public Action OnItemDrop;
    public Action<int> PlusQuestCount;
    

    /// <summary>
    /// ���Ͱ� ���� ���ߴ��� �˸��� �̺�Ʈ �Լ�
    /// </summary>
    /// <param name="attackedMonster">���ݴ��� ����</param>
    public void MonsterAttacked(Monster_Base attackedMonster)
    {
        OnMonsterAttacked?.Invoke(attackedMonster);
    }
   

  
}