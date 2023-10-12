using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    public GameObject chestPrefab; // ������ ���� ������
    public Vector3 spawnPosition; // ���ڰ� ������ ��ġ
    private void Start()
    {
        SpawnChest();
    }

    void SpawnChest()
    {
        Instantiate(chestPrefab, spawnPosition, Quaternion.identity);
    }
}
