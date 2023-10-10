using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Base : PooledObject
{
    /// <summary>
    /// 주변 몬스터가 공격받앗는지에 대한 여부를 묻는 bool타입( true일 때 주변 몬스터가 공격받음)
    /// </summary>
    public bool isFriendsAttacked;

    /// <summary>
    /// 몬스터의 HP변화를 알리는 델리게이트 UI 연동용
    /// </summary>
    public Action<float> onHealthChange { get; set; }

    /// <summary>
    /// 몬스터가 플레이어를 감지했을때 Detect_State 진입용 함수
    /// </summary>
    public virtual void Detect()
    {
       
    }
}
