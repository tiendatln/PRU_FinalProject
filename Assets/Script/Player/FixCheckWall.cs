using UnityEngine;

public class FixCheckWall : MonoBehaviour
{
     [SerializeField] private Transform target; // Gán object cha vào đây

    private Vector3 offset;

    void Start()
    {
        // Tính khoảng cách ban đầu giữa object con và cha
        offset = transform.position - target.position;
    }

    void Update()
    {
        // Chỉ cập nhật vị trí, không ảnh hưởng rotation
        transform.position = target.position + offset;
    }
}
