using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class IncludingStatsActor : MonoBehaviour
{
    protected bool isAlive = true;

    float maxHP = 1000.0f;
    public float MaxHP => maxHP;

    public Action<float> onHealthChange { get; set; }

    //protected int attackPoint;
    protected float criticalChance;
    protected int defPoint;

    
    public float hp;
    public float HP
    {
        get => hp;
        set
        {
            if(value != hp)
            {
                hp = value;
                onHealthChange?.Invoke(hp);
                if (hp <= 0)
                {
                    Die();
                }
            }
        }

    }
    protected float def;


    protected float atk;

    protected float calculatedAttackPower;

    public float CalculatedAttackPower
    {
        get => calculatedAttackPower;
        set
        {
            if (calculatedAttackPower != value)
            {
                calculatedAttackPower = value;
            }
        }

    }

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

    public virtual void OnDamage(float damage, bool knockback, Vector3 attackPos)
    {

    }

    //protected virtual void OnDamage(int damage, float criChance)
    //{
    //    int minDamage = (int)Mathf.Round(damage * -0.1f) + damage;
    //    int maxDamage = (int)Mathf.Round(damage * 0.1f) + damage;

    //    int ranDamage = UnityEngine.Random.Range(minDamage, maxDamage);

    //    if (UnityEngine.Random.Range(0.0f, 100.0f) < criticalChance)
    //    {

    //        hp -= ranDamage;
    //        ranDamage += (int)(ranDamage * 0.3f);
    //    }
    //}
    protected virtual void Die()
    {
        Debug.Log("사망");
    }
}
