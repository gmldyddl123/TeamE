using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MelleAttackCollider : MonoBehaviour
{
    public float atkPower { private get; set; }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<IncludingStatsActor>().OnDamage(atkPower);
        }
    }

}
