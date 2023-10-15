using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public Func<float> atkPower;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            //other.GetComponent<IncludingStatsActor>().OnDamage(atkPower.Invoke()) ;
            Debug.Log(atkPower.Invoke());

            other.GetComponent<Monster_Base>().OnDamage(atkPower.Invoke());
        }
    }

}
