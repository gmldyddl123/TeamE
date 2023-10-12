using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    public bool IsDirectUse => true;
    public Action onItemDrop;
    public ItemData itemData; // ������ ������ ����

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
            Debug.LogError("ItemData�� �Ҵ���� �ʾҽ��ϴ�.");
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