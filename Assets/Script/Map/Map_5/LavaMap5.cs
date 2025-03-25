using UnityEngine;

public class LavaMap5 : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.getMainData().health -= 1f;
        }
    }
}
