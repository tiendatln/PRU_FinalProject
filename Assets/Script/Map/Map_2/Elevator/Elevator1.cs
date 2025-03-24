using UnityEngine;

public class Elevator1 : MonoBehaviour
{
    public float speed = 2f;  // Tốc độ di chuyển
    public Transform pointA, pointB; // Hai điểm giới hạn

    private Vector3 target;

    void Start()
    {
        target = pointB.position; // Bắt đầu hướng tới điểm B
    }

    void Update()
    {
        // Di chuyển thang máy về điểm đích
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Đảo chiều khi chạm đến 1 trong 2 điểm
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            target = (target == pointA.position) ? pointB.position : pointA.position;
        }
    }
}
