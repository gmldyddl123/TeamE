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
            if (IsAlive)       // ������� ���� HP ����
            {
                hp = value;
                if (hp <= 0)   // hp�� 0 ���ϸ� ���
                {
                    Die();
                    IsAlive = false;
                }
                hp = Mathf.Clamp(hp, 0, MaxHP);     // HP�� �׻� 0~�ִ�ġ
                onHealthChange?.Invoke(hp / MaxHP);   // HP ��ȭ �˸���
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
        Debug.Log("���");
    }
}
