using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Sword_1 : MonoBehaviour
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
            monsterStatsActor.OnDamage(75, 0);
        }
    }
}
