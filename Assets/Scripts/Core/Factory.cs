using monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolObjectType
{
    Melee_Monster = 0,
    Long_Monster
}

public class Factory : Singleton<Factory>
{
    //public GameObject playerBullet;
    M_MonsterPool m_MonsterPool;

    //HitPool hitPool;
    //ExplosionPool expsionPool;
    //BossPool bossPool;
    //BossBulletPool bossBulletPool;
    //BossMissiletPool bossMissiletPool;
    //EnemyAsteroidPool enemyAsteroidPool;
    //EnemyAsteroidMiniPool enemyAsteroidMiniPool;
    //EnemyCurvePool enemyCurvePool;
    //EnemyFighterPool enemyFighterPool;
    //EnemyStrikePool enemyStrikePool;
    //PowerUpPool powerUpPool;

    protected override void OnInitialize()
    {
        base.OnInitialize();

        m_MonsterPool = GetComponentInChildren<M_MonsterPool>();

        m_MonsterPool?.Initialize();
       
    }

    /// <summary>
    /// 오브젝트를 풀에서 하나 가져오는 함수
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public GameObject GetObject(PoolObjectType type)
    {
        GameObject result;
        switch (type)
        {
              case PoolObjectType.Melee_Monster:
              result = m_MonsterPool?.GetObject()?.gameObject;                
               break;
             default:
              result = new GameObject();
              break;
        }

        return result;
    }

    /// <summary>
    /// 오브젝트를 풀에서 하나 가져오면서 위치와 각도를 설정하는 함수
    /// </summary>
    /// <param name="type">생성할 오브젝트의 종류</param>
    /// <param name="position">생성할 위치(월드좌표)</param>
    /// <returns>생성한 오브젝트</returns>
    public GameObject GetObject(PoolObjectType type, Vector3 position)
    {
        GameObject obj = GetObject(type);
        obj.transform.position = position;
        return obj;
    }
}
