using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    public GameObject chestPrefab; // 생성할 상자 프리팹

    public void SpawnChest(Vector3 spawnPosition)
    {
        // y 축에 0.6을 추가하여 상자가 땅에서 약간 높은 곳에 생성되도록 조정
        Vector3 adjustedPosition = new Vector3(spawnPosition.x, spawnPosition.y + 0.6f, spawnPosition.z);

        // 수정된 위치에 상자 프리팹을 생성
        Instantiate(chestPrefab, adjustedPosition, Quaternion.identity);
    }
}