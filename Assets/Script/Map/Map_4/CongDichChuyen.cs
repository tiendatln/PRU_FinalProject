using UnityEngine;

public class CongDichChuyen : MonoBehaviour
{
    [SerializeField] Transform DiemDichChuyenDen; // Điểm đến sau khi dịch chuyển
    [SerializeField] GameObject Boss;
    private GameObject Spawnpoint;
    public Transform GetDiemDichChuyen()
    {
        return DiemDichChuyenDen;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Spawnpoint = GameObject.Find("Spawn point");
        // Nếu nhân vật có tag "Player" chạm vào
        if (collision.CompareTag("Player"))
        {
            // Dịch chuyển nhân vật tới điểm đích
            collision.transform.position = DiemDichChuyenDen.position;
            Instantiate(Boss, Spawnpoint.transform.position, Boss.transform.rotation);
        }
    }
}
