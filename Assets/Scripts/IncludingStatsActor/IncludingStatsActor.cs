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
    /// 공격시 살짝 움직이는것
    /// </summary>
    /// <param name="movedir">이동 방향 근접은 앞으로 원거리는 뒤로 움직인다</param>
    public virtual void AttackMove(Vector3 movedir)
    {

    }

    public virtual void OnDamage(float damage)
    {
        float totalDamage = Mathf.Clamp(damage, 1.0f, damage- def);
        HP -= totalDamage;
        Debug.Log($"피격! {totalDamage}피해, 남은 hp {HP}");
    }

    protected void Die()
    {
        Debug.Log("사망");
    }
}
