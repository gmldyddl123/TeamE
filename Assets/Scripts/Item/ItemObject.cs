using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    public bool IsDirectUse => true;
    public Action onItemDrop;
    public ItemData itemData; // 아이템 데이터 참조

    void Start()
    {
        UpdateParticleColors();
    }

    void UpdateParticleColors()
    {
        if (itemData != null)
        {
            Color colorForGrade = itemData.gradeColor[itemData.itemgrade];

            ParticleSystem[] particleSystems = GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem ps in particleSystems)
            {
                var main = ps.main;
                main.startColor = colorForGrade;
            }
        }
        else
        {
            Debug.LogError("ItemData가 할당되지 않았습니다.");
        }
    }

    public event Action<IInteractable> OnDestroyed;

    public void Use()
    {
        Inventory.instance.Add(itemData);
        OnDestroyed?.Invoke(this);
        Destroy(gameObject);
    }
}