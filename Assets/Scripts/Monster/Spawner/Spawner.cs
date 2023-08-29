

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
    /// ������ �������� ��� �ð�
    /// </summary>
    float elapsed = 0.0f;
    /// <summary>
    /// ���� ����
    /// </summary>
    public float interval = 10.0f;

   

    /// <summary>
    /// ���������� ������ ������ġ
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
    /// ���� �����ϴ� �Լ�
    /// </summary>
    private void Spawn()
    {
        int index = UnityEngine.Random.Range(0, spawnPos.Length - 1);
        Vector3 spawnPosition = spawnPos[index].position;
        //if (!CheckSpawnPosition(spawnPosition))
        //{
        //    Debug.Log("���� ��ġ �ֺ��� �ٸ� ������Ʈ�� �־� ������ ����մϴ�.");
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

        // ���� ��ġ �ֺ��� �ٸ� ������Ʈ�� �ִ��� Ȯ��
        foreach (Collider collider in colliders)
        {
                Debug.Log("���� ��ġ �ֺ��� �ٸ� ������Ʈ�� �ֽ��ϴ�.");
            Debug.Log(colliders);
                return false; 
        }

        // ���� ��ġ �ֺ��� �ٸ� ������Ʈ�� ����
        return true;
    }

   

}

