using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    public GameObject chestPrefab; // ������ ���� ������

    public void SpawnChest(Vector3 spawnPosition)
    {
        // y �࿡ 0.6�� �߰��Ͽ� ���ڰ� ������ �ణ ���� ���� �����ǵ��� ����
        Vector3 adjustedPosition = new Vector3(spawnPosition.x, spawnPosition.y + 0.6f, spawnPosition.z);

        // ������ ��ġ�� ���� �������� ����
        Instantiate(chestPrefab, adjustedPosition, Quaternion.identity);
    }
}