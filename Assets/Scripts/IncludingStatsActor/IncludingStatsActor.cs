using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncludingStatsActor : MonoBehaviour
{
    //����

    /// <summary>
    /// �÷��̾� ���� ����
    /// </summary>
    public bool IsAlive => hp > 0;

    /// <summary>
    /// ���� HP
    /// </summary>
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
                    //Die();
                }
                hp = Mathf.Clamp(hp, 0, MaxHP);     // HP�� �׻� 0~�ִ�ġ
                onHealthChange?.Invoke(hp / MaxHP);   // HP ��ȭ �˸���
            }
        }
    }

    /// <summary>
    /// �ִ� HP
    /// </summary>
    float maxHP = 1000.0f;
    public float MaxHP => maxHP;

    /// <summary>
    /// HP�� ����Ǿ��� �� ����� ��������Ʈ
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
