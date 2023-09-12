using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncludingStatsActor : MonoBehaviour
{
    //스탯

    /// <summary>
    /// 플레이어 생존 여부
    /// </summary>
    public bool IsAlive => hp > 0;

    /// <summary>
    /// 현재 HP
    /// </summary>
    float hp = 1000.0f;
    public float HP
    {
        get => hp;
        set
        {
            if (IsAlive)       // 살아있을 때만 HP 변경
            {
                hp = value;
                if (hp <= 0)   // hp가 0 이하면 사망
                {
                    //Die();
                }
                hp = Mathf.Clamp(hp, 0, MaxHP);     // HP는 항상 0~최대치
                onHealthChange?.Invoke(hp / MaxHP);   // HP 변화 알리기
            }
        }
    }

    /// <summary>
    /// 최대 HP
    /// </summary>
    float maxHP = 1000.0f;
    public float MaxHP => maxHP;

    /// <summary>
    /// HP가 변경되었을 때 실행될 델리게이트
    /// </summary>
    public Action<float> onHealthChange { get; set; }

    protected int attackPoint;
    protected float criticalChance;
    protected int defPoint;


    public virtual void Attack(Vector3 movedir)
    {

    }

    protected virtual void OnDamage(int damage, float criChance)
    {
        int minDamage = (int)Mathf.Round(damage * -0.1f) + damage;
        int maxDamage = (int)Mathf.Round(damage * 0.1f) + damage;

        int ranDamage = UnityEngine.Random.Range(minDamage, maxDamage);

        if (UnityEngine.Random.Range(0.0f, 100.0f) < criticalChance)
        {

        hp -= ranDamage;
            ranDamage += (int)(ranDamage * 0.3f);
        }
    }
}
