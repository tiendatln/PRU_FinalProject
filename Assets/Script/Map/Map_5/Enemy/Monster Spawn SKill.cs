using UnityEngine;

public class MonsterSpawnSKill : MonoBehaviour
{
    public GameObject skil;
    public Transform SpwanPoint;
    public float SkillSpeed;
    public float SkillCoodDown;
    public Transform player; // Gán Player vào Inspector
    public float rotationSpeed = 5f;

   
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Update()
    {
        SkillCoodDown -= Time.deltaTime;

        if (player != null)
        {
            // Tính hướng quay
            Vector3 direction = (player.position - transform.position).normalized;

            // Tạo góc quay mới nhưng giữ nguyên trục Y
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));

            // Xoay 90 độ quanh trục Y để trục X hướng về player
            lookRotation *= Quaternion.Euler(0, -90, 0);

            // Quay dần về hướng Player
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }


    }

    public virtual void Shoot()
    {
        
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, SpwanPoint.position);
            GameObject magicSkill = Instantiate(skil, SpwanPoint.position, SpwanPoint.rotation);
            magicSkill.gameObject.SetActive(true);
            // Lấy Rigidbody2D của đạn để áp dụng lực
            Rigidbody2D rb = magicSkill.GetComponent<Rigidbody2D>();

            // Áp dụng lực cho viên đạn để nó di chuyển theo hướng firePoint
            rb.AddForce(SpwanPoint.right * SkillSpeed, ForceMode2D.Impulse);
            Destroy(magicSkill, 1);
            SkillCoodDown = 2f;
        
    }
}
