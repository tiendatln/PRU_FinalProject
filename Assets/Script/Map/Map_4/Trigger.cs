using UnityEngine;

public class TriggerDisableObject : MonoBehaviour
{
    public GameObject objectToDisable; // Kéo object cần tắt/mở vào đây

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Khi Player vào vùng trigger
        {
            if (objectToDisable != null)
            {
                objectToDisable.SetActive(false); // Tắt object
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Khi Player rời khỏi vùng trigger
        {
            if (objectToDisable != null)
            {
                objectToDisable.SetActive(true); // Mở lại object
            }
        }
    }
}
