using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class IncludingStatsActor : MonoBehaviour
{
    public float hp;
    public float HP
    {
        get => hp;
        set
        {
            if(value != hp)
            {
                hp = value;
                if (hp <= 0)
                {
                    Die();
                }
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

    /// <summary>
    /// ���ݽ� ��¦ �����̴°�
    /// </summary>
    /// <param name="movedir">�̵� ���� ������ ������ ���Ÿ��� �ڷ� �����δ�</param>
    public virtual void AttackMove(Vector3 movedir)
    {

    }

    public virtual void OnDamage(float damage)
    {
        float totalDamage = Mathf.Clamp(damage, 1.0f, damage- def);
        HP -= totalDamage;
        Debug.Log($"�ǰ�! {totalDamage}����, ���� hp {HP}");
    }

    protected void Die()
    {
        Debug.Log("���");
    }
}
