using UnityEngine;
using System.Collections;
public class DoorTeleport : MonoBehaviour
{
    public DoorTeleport targetDoor; // Cửa đích đến
    public Transform teleportPoint; // Vị trí dịch chuyển khi vào cửa
    private bool isTeleporting = false; // Để tránh dịch chuyển liên tục

    private void Start()
    {
        if (teleportPoint == null)
        {
            teleportPoint = transform; // Nếu không có, dùng vị trí của chính nó
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTeleporting)
        {
            if (targetDoor != null)
            {
                StartCoroutine(Teleport(other.transform, targetDoor.teleportPoint.position - new Vector3(2, 0, 0)));
            }
        }
    }

    private IEnumerator Teleport(Transform player, Vector3 targetPosition)
    {
        isTeleporting = true;

        // Dịch chuyển người chơi
        player.position = targetPosition;

        // Đợi một chút để tránh loop dịch chuyển giữa hai cửa
        yield return new WaitForSeconds(0.5f);

        isTeleporting = false;
    }
}
