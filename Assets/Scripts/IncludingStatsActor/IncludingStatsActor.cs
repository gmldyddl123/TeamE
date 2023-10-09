using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncludingStatsActor : MonoBehaviour
{
    public bool IsAlive = true;
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
                    Die();
                    IsAlive = false;
                }
                hp = Mathf.Clamp(hp, 0, MaxHP);     // HP는 항상 0~최대치
                onHealthChange?.Invoke(hp / MaxHP);   // HP 변화 알리기
            }
        }
    }
    protected float def;

    public float atk;

    public float ATK
    {
        get => atk;
        set
        {
            if (atk != value)
            {
                atk = value;
            }
        }

    }

    float maxHP = 1000.0f;
    public float MaxHP => maxHP;

    public Action<float> onHealthChange { get; set; }

    protected int attackPoint;
    protected float criticalChance;
    protected int defPoint;


    /// <summary>
    /// 공격시 살짝 움직이는것
    /// </summary>
    /// <param name="movedir">이동 방향 근접은 앞으로 원거리는 뒤로 움직인다</param>
    public virtual void AttackMove(Vector3 movedir)
    {

    }


    public virtual void OnDamage(float damage)
    {
        float totalDamage = Mathf.Clamp(damage, 1.0f, damage - def);
        HP -= totalDamage;
        Debug.Log($"피격! {totalDamage}피해, 남은 hp {HP}");
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
    protected void Die()
    {
        Debug.Log("사망");
    }
}
