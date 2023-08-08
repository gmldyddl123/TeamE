using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncludingStatsActor : MonoBehaviour
{
    //Ω∫≈»

    //protected int hpMax;
    //protected int hp;

    //protected int attackPoint;
    //protected float criticalChance;
    //protected int defPoint;


    public virtual void Attack(Vector3 movedir)
    {

    }

    //protected virtual void OnDamage(int damage, float criChance)
    //{
    //    int minDamage = (int)Mathf.Round(damage * -0.1f) + damage;
    //    int maxDamage = (int)Mathf.Round(damage * 0.1f) + damage;

    //    int ranDamage = Random.Range(minDamage, maxDamage);

    //    if(Random.Range(0.0f, 100.0f) < criticalChance )
    //    {
    //        ranDamage += (int)(ranDamage * 0.3f);
    //    }

    //    hp -= ranDamage;
    //}
}
