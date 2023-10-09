using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Weapon : MonoBehaviour
{
    IncludingStatsActor monsterStatsActor;

    private void Awake()
    {
        monsterStatsActor = FindObjectOfType<IncludingStatsActor>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            monsterStatsActor.OnDamage(35);
        }
    }
}
