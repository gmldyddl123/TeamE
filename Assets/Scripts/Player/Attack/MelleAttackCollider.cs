using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MelleAttackCollider : MonoBehaviour
{
    public Func<float> atkPower;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<IncludingStatsActor>().OnDamage(atkPower.Invoke()) ;
        }
    }

}
