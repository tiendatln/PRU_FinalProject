using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public GameObject pyramidDoor; // Gán Pyramid_Door vào đây trong Unity

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Kiểm tra nếu người chơi chạm vào
        {
            Destroy(pyramidDoor); // Xóa Pyramid_Door
            Destroy(gameObject);  // Xóa chính nó (P_Key)
        }
    }
}
