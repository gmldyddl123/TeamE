using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    public GameObject chestPrefab; // 생성할 상자 프리팹
    public Vector3 spawnPosition; // 상자가 생성될 위치
    private void Start()
    {
        SpawnChest();
    }

    void SpawnChest()
    {
        Instantiate(chestPrefab, spawnPosition, Quaternion.identity);
    }
}
