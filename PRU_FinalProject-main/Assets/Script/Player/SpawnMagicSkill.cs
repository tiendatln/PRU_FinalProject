using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SpawnMagicSkill : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab của đạn
    public Transform firePoint;     // Vị trí nơi đạn được bắn ra
    public float bulletSpeed = 10f; // Tốc độ đạn


    public virtual void Shoot(float face)
    {
        if (face > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (face < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
            // Tạo đạn tại vị trí của firePoint
            GameObject magicSkill = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        magicSkill.gameObject.SetActive(true);
        // Lấy Rigidbody2D của đạn để áp dụng lực
        Rigidbody2D rb = magicSkill.GetComponent<Rigidbody2D>();
        
        // Áp dụng lực cho viên đạn để nó di chuyển theo hướng firePoint
        rb.AddForce(firePoint.right * bulletSpeed, ForceMode2D.Impulse);
        Destroy(magicSkill, 1);
    }
}
