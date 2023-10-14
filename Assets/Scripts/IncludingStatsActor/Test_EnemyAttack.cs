using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_EnemyAttack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().currentPlayerCharacter.OnDamage(1);
        }
        
    }
}
