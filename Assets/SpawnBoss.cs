using UnityEngine;

public class SpawnBoss : MonoBehaviour
{

    public GameObject spawnBoss;


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(spawnBoss, this.transform.position, spawnBoss.transform.rotation);
        }
    }
}
