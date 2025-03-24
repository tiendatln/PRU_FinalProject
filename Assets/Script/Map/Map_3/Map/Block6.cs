using UnityEngine;

public class Block6 : MonoBehaviour
{
    public float moveSpeed = 2f; // Tốc độ di chuyển sang phải
    private bool shouldMove = false; // Kiểm tra block có đang di chuyển không

    void Update()
    {
        if (shouldMove)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            shouldMove = true; // Khi chạm vào Player, block sẽ bắt đầu di chuyển
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            shouldMove = false; // Khi chạm vào Ground, block sẽ dừng lại
        }
    }
}
