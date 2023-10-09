using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Weapon : MonoBehaviour
{
    MonsterStatsActor monsterStatsActor;

    private void Awake()
    {
        monsterStatsActor = FindObjectOfType<MonsterStatsActor>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            monsterStatsActor.OnDamage(35, 0);
        }
    }
}
