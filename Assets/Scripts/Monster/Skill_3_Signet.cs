using boss;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_3_Signet : MonoBehaviour
{
    Collider coll;
    Boss_Monster boss;

    private void Awake()
    {
        coll = GetComponent<Collider>();
        boss = GetComponentInParent<Boss_Monster>();
    }
    private void Start()
    {
        boss.isSkill_3_Hit_Start += ColliderOn;
        boss.isSkill_3_Hit_Finish += ColliderOff;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("ÂïÇú´Ù~");
        }
    }

    void ColliderOn()
    {
        coll.enabled = true;
    }
    
    void ColliderOff()
    {
        coll.enabled = false;
    }
}
