using player;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public Func<float> atkPower;

    public Action skillGaugeUp;

    private void Awake()
    {
        //���� �ڵ�
        skillGaugeUp = () =>  FindAnyObjectByType<PlayerController>().currentPlayerCharacter.CurrentSkillGauge += 20.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            other.GetComponent<Monster_Base>().OnDamage(atkPower.Invoke());

            skillGaugeUp?.Invoke();
        }
    }

}
