using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    public GameObject BossPrefab;
    private GameObject SpawmPoint;
    public AudioClip bg;
    private BoxCollider2D triggerCollider;

    void Start()
    {
        // Gán Collider đúng cách trong Start()
        triggerCollider = GetComponent<BoxCollider2D>();

        if (triggerCollider == null)
        {
            Debug.LogError("⚠ Không tìm thấy BoxCollider2D trên " + gameObject.name);
        }

        FindSpawmPoint();
    }

    public void playMusic()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.stopMusicLoopSound();
            AudioManager.Instance.playMusicLoopSound(bg);
        }
    }

    public void FindSpawmPoint()
    {
        SpawmPoint = GameObject.Find("Spawm Point");
        if (SpawmPoint == null)
        {
            Debug.LogError("⚠ Không tìm thấy Spawm Point trong scene!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (SpawmPoint != null)
            {
                Vector3 summonPos = SpawmPoint.transform.position;
                Instantiate(BossPrefab, summonPos, BossPrefab.transform.localRotation);
            }

            playMusic();

            if (triggerCollider != null)
            {
                Debug.Log("✅ Đã tắt Trigger Collider!");
                triggerCollider.enabled = false;
            }
            else
            {
                Debug.LogError("❌ triggerCollider vẫn null!");
            }
        }
    }
}
