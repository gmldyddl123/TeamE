using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 오브젝트 풀에 들어갈 오브젝트들이 상속받을 클래스
/// </summary>
public class PooledObject : MonoBehaviour
{
    /// <summary>
    /// 이 게임 오브젝트가 비활성화 될 때 실행되는 델리게이트
    /// </summary>
    public Action onDisable;


    protected virtual void OnDisable()
    {
        onDisable?.Invoke();    // 비활성화 되었다고 알림
        gameObject.SetActive(false);
    }

   

   
}
