
using monster;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.Port;

public class Spawner : MonoBehaviour
{
    Transform[] spawnPos;
    int spawnCount = 0;
    int maxSpawnCount;
    float spawnCheckRadius = 1.0f;
    /// <summary>
    /// ������ �������� ��� �ð�
    /// </summary>
    float elapsed = 0.0f;
    /// <summary>
    /// ���� ����
    /// </summary>
    public float interval = 10.0f;

    /// <summary>
    /// ���� ��ġ�� �����ص� �迭�� ������
    /// </summary>
    int index;

    /// <summary>
    /// ���������� ������ ������ġ
    /// </summary>
    Vector3 spawnPosition;
    private void Awake()
    {  
        spawnPos = new Transform[transform.childCount - 1];
        for (int i = 0; i < spawnPos.Length; i++)
        {
            spawnPos[i] = transform.GetChild(i);
        }
        maxSpawnCount = spawnPos.Length;

        index = UnityEngine.Random.Range(0, spawnPos.Length - 1);
        spawnPosition = spawnPos[index].position;
        
    }
   
     private void FixedUpdate()
    {
        if (spawnCount < maxSpawnCount)
        {
            elapsed += Time.fixedDeltaTime;
            if (elapsed > interval)
            {
                Spawn();
                elapsed = 0.0f;

                spawnCount++;
            }
        }
    }
    /// <summary>
    /// ���� �����ϴ� �Լ�
    /// </summary>
    private void Spawn()
    {
        if (!CheckSpawnPosition(spawnPosition))
        {
            Debug.Log("���� ��ġ �ֺ��� �ٸ� ������Ʈ�� �־� ������ ����մϴ�.");
            return;
        }
        else
        { 
            Factory.Inst.GetObject(PoolObjectType.Melee_Monster, spawnPosition);
        }
    }

    bool CheckSpawnPosition(Vector3 spawnPosition)
    {
        Collider[] colliders = Physics.OverlapSphere(spawnPosition, spawnCheckRadius);

        // ���� ��ġ �ֺ��� �ٸ� ������Ʈ�� �ִ��� Ȯ��
        foreach (Collider collider in colliders)
        {
                Debug.Log("���� ��ġ �ֺ��� �ٸ� ������Ʈ�� �ֽ��ϴ�.");
                return false; 
        }

        // ���� ��ġ �ֺ��� �ٸ� ������Ʈ�� ����
        return true;
    }


}

