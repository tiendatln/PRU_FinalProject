using UnityEngine;

public class Block1 : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke("DisableBlock", 0.2f);
        }
    }

    private void DisableBlock()
    {
        Destroy(gameObject);
    }
}
