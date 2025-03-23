using UnityEngine;

public class DeadMap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.getMainData().health = 0;
        }
    }
}
