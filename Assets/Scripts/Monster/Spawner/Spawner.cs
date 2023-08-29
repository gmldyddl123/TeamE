

using monster;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    Transform[] spawnPos;
    MonsterEvent monsterEvents;
    
    int spawnCount;
  
    public int maxSpawnCount = 5;
    

    float spawnCheckRadius = 2.0f;
    /// <summary>
    /// 마지막 스폰에서 경과 시간
    /// </summary>
    float elapsed = 0.0f;
    /// <summary>
    /// 스폰 간격
    /// </summary>
    public float interval = 10.0f;

   

    /// <summary>
    /// 랜덤값으로 정해진 스폰위치
    /// </summary>
   
    private void Awake()
    {
        spawnCount = 0;
        spawnPos = new Transform[transform.childCount - 1];
        for (int i = 0; i < spawnPos.Length; i++)
        {
            spawnPos[i] = transform.GetChild(i);
        }

       

    }
    private void Start()
    {
        monsterEvents = FindObjectOfType<MonsterEvent>();
        monsterEvents.SpawnCountChange += ChangeSpawnCount;
        if (spawnCount < maxSpawnCount)
        {
                FirstSpawn();
            spawnCount += maxSpawnCount;  
        }
        
    }

    private void ChangeSpawnCount()
    {
        spawnCount--;
    }

    private void Update()
    {
        if (spawnCount < maxSpawnCount)
        {
            elapsed += Time.deltaTime;
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
        int index = UnityEngine.Random.Range(0, spawnPos.Length - 1);
        Vector3 spawnPosition = spawnPos[index].position;
        //if (!CheckSpawnPosition(spawnPosition))
        //{
        //    Debug.Log("스폰 위치 주변에 다른 오브젝트가 있어 스폰을 대기합니다.");
        //    Debug.Log($"{spawnPos.Length}");
        //    return;
        //}
        //else
        //{
            GameObject monsterObject = Factory.Inst.GetObject(PoolObjectType.Melee_Monster, spawnPosition);
            Monster monster = monsterObject.GetComponent<Monster>();
            if (monster != null)
            {
                monster.SpawnPosition = spawnPosition;
            
            }
        //}
    }

    void FirstSpawn()
    {
      
        for (int i = 0; i < maxSpawnCount; i++)
        {
            int randomindex = Random.Range(0, spawnPos.Length - 1);
            Vector3 spawnPosition = spawnPos[randomindex].position;
            GameObject monsterObject = Factory.Inst.GetObject(PoolObjectType.Melee_Monster, spawnPosition);
            Monster monster = monsterObject.GetComponent<Monster>();
            if (monster != null)
            {
                monster.SpawnPosition = spawnPosition;
                
            }

        }
    }

    bool CheckSpawnPosition(Vector3 spawnPosition)
    {
        Collider[] colliders = Physics.OverlapSphere(spawnPosition, spawnCheckRadius);

        // 스폰 위치 주변에 다른 오브젝트가 있는지 확인
        foreach (Collider collider in colliders)
        {
                Debug.Log("스폰 위치 주변에 다른 오브젝트가 있습니다.");
            Debug.Log(colliders);
                return false; 
        }

        // 스폰 위치 주변에 다른 오브젝트가 없음
        return true;
    }

   

}

