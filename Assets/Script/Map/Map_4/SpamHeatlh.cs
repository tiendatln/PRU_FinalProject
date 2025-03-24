using UnityEngine;
using System.Collections;

public class HealthSpawner : MonoBehaviour
{
    [Header("Gán Prefab vào đây")]
    public GameObject healthPrefab; // Prefab health

    public int numberOfHealth = 3; // Số lượng health sẽ spawn mỗi lần
    public float spawnRadius = 1.5f; // Bán kính random spawn health
    public float spawnInterval = 5f; // Thời gian giữa các lần spawn

    void Start()
    {
        StartCoroutine(SpawnHealthLoop()); // Bắt đầu Coroutine lặp lại
    }

    IEnumerator SpawnHealthLoop()
    {
        while (true) // Vòng lặp vô hạn
        {
            SpawnHealth();
            yield return new WaitForSeconds(spawnInterval); // Chờ 5 giây trước khi spawn lần tiếp theo
        }
    }

    void SpawnHealth()
    {
        if (healthPrefab == null)
        {
            Debug.LogError("⚠️ healthPrefab chưa được gán!");
            return;
        }

        for (int i = 0; i < numberOfHealth; i++)
        {
            Vector3 spawnPosition = transform.position + (Vector3)Random.insideUnitCircle * spawnRadius;
            Instantiate(healthPrefab, spawnPosition, Quaternion.identity);
        }

        Debug.Log($"✅ Spawned {numberOfHealth} Health items!");
    }
}
