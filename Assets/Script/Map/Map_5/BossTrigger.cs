using System.Collections;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject boss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Spwan());
        }
    }

    private IEnumerator Spwan()
    {
        yield return null;
        Instantiate(boss, this.transform.position, boss.transform.rotation);
    }
}
