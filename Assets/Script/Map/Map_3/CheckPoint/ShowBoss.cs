using UnityEngine;

public class ShowBoss : MonoBehaviour
{
    public GameObject bossPrefab; // Gán prefab của boss vào Inspector
    public Transform spawnPoint; // Vị trí xuất hiện của boss

    public AudioClip fightSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (bossPrefab != null && spawnPoint != null)
            {
                Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);
                Debug.Log("spam boss");
            }
            else
            {
                Debug.LogWarning("Boss Prefab hoặc Spawn Point chưa được gán!");
            }

            AudioManager.Instance.playMusicLoopSound(fightSound);

            Destroy(this);

        }
    }
}
