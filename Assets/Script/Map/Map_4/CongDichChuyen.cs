using UnityEngine;

public class CongDichChuyen : MonoBehaviour
{
    [SerializeField] Transform DiemDichChuyenDen; // Điểm đến sau khi dịch chuyển
    public Transform GetDiemDichChuyen()
    {
        return DiemDichChuyenDen;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Nếu nhân vật có tag "Player" chạm vào
        if (collision.CompareTag("Player"))
        {
            // Dịch chuyển nhân vật tới điểm đích
            collision.transform.position = DiemDichChuyenDen.position;
        }
    }
}
