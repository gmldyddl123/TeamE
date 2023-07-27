
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
    /// 마지막 스폰에서 경과 시간
    /// </summary>
    float elapsed = 0.0f;
    /// <summary>
    /// 스폰 간격
    /// </summary>
    public float interval = 10.0f;

    /// <summary>
    /// 스폰 위치를 저장해둔 배열의 랜덤값
    /// </summary>
    int index;

    /// <summary>
    /// 랜덤값으로 정해진 스폰위치
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
    /// 몬스터 스폰하는 함수
    /// </summary>
    private void Spawn()
    {
        if (!CheckSpawnPosition(spawnPosition))
        {
            Debug.Log("스폰 위치 주변에 다른 오브젝트가 있어 스폰을 대기합니다.");
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

        // 스폰 위치 주변에 다른 오브젝트가 있는지 확인
        foreach (Collider collider in colliders)
        {
                Debug.Log("스폰 위치 주변에 다른 오브젝트가 있습니다.");
                return false; 
        }

        // 스폰 위치 주변에 다른 오브젝트가 없음
        return true;
    }


}

