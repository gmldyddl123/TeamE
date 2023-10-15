using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class IncludingStatsActor : MonoBehaviour
{
    protected bool isAlive = true;
    public bool IsAlive
    {
        get => isAlive;
    }


    protected float maxHP = 1000.0f;
    public float MaxHP => maxHP;

    public Action<float> onHealthChange { get; set; }

    //protected int attackPoint;
    //protected float criticalChance;
    //protected int defPoint;

    
    public float hp;
    public float HP
    {
        get => hp;
        set
        {
            if(value != hp)
            {
                
                hp = Mathf.Clamp(value, 0, maxHP);
                Debug.Log(hp);
                if (hp <= 0)
                {
                    Die();
                }
                onHealthChange?.Invoke(hp);
            }
        }

    }
    protected float def;

    public float Def
    {
        get => def;
        set
        {
            if (value != def)
            {
                def = value;
            }
        }
    }


    protected float atk;

    public float Atk
    {
        get => atk;
        set
        {
            if (value != atk)
            {
                atk = value;
            }
        }
    }


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
    /// ���ݽ� ��¦ �����̴°�
    /// </summary>
    /// <param name="movedir">�̵� ���� ������ ������ ���Ÿ��� �ڷ� �����δ�</param>
    public virtual void AttackMove(Vector3 movedir)
    {

    }

    public virtual void OnDamage(float damage)
    {
        float totalDamage = Mathf.Clamp(damage, 1.0f, damage - def);
        HP -= totalDamage;
        Debug.Log($"�ǰ�! {totalDamage}����, ���� hp {HP}");
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
        Debug.Log("���");
    }
}
