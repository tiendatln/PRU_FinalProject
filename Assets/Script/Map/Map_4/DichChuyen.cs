using UnityEngine;

public class DichChuyen : MonoBehaviour
{
    [SerializeField] GameObject Cong;

    void Update()
    {
        // Kiểm tra nếu có cổng dịch chuyển hợp lệ
        if (Cong != null)
        {
            Transform diemDichChuyen = Cong.GetComponent<CongDichChuyen>().GetDiemDichChuyen();
            if (diemDichChuyen != null)
            {
                transform.position = diemDichChuyen.position;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Chỉ dịch chuyển khi đối tượng có tag "Player"
        if (collision.CompareTag("Player"))
        {
            Cong = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Khi rời khỏi cổng dịch chuyển thì đặt lại
        if (collision.CompareTag("Player"))
        {
            Cong = null;
        }
    }
}
