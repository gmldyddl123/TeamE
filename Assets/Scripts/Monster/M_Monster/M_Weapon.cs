using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Weapon : MonoBehaviour
{
    IncludingStatsActor monsterStatsActor;
    public Collider col;

    private void Awake()
    {
        monsterStatsActor = FindObjectOfType<IncludingStatsActor>();
        col = GetComponent<Collider>();
        col.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            monsterStatsActor.OnDamage(35);
        }
    }
}
