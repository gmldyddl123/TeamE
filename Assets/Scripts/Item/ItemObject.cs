using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    ItemData item;

    public bool IsDirectUse => true;

    public void Use()
    {
        // 등급별 드랍 확률을 적용하여 랜덤으로 아이템 추가
        float dropChance = UnityEngine.Random.Range(0f, 1f);
        ItemGrade itemGrade = item.itemgrade;
        if (itemGrade != ItemGrade.None && item.gradeToStars.ContainsKey(itemGrade))
        {
            string gradeStars = item.gradeToStars[itemGrade];
            if (dropChance < item.gradeDropChances[itemGrade])
            {
                Inventory.instance.Add(item);
            }
        }
        Destroy(gameObject);
    }
}