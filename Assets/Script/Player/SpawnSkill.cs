using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SpawnMagicSkill : MonoBehaviour
{
    public GameObject MagicSkill; // Prefab của đạn
    public GameObject Arrows;
    public Transform firePoint;     // Vị trí nơi đạn được bắn ra
    public float MagicSpeed; // Tốc độ đạn
    public float ArrowSpeed;

    [Header("Skill Time")]
    public float SetTimeArrowSkill;
    public float SetTimeMagicSkill;

    public float MagicCoolDownSkill;
    public float ArrowCoolDownSkill;

    private void Update()
    {
        MagicCoolDownSkill -= Time.deltaTime;
        ArrowCoolDownSkill -= Time.deltaTime;
    }

    public virtual void ArrowShoot()
    {
        if (ArrowCoolDownSkill < 0)
        {
            
            GameObject arrow = Instantiate(Arrows, firePoint.position, firePoint.rotation);
            arrow.gameObject.SetActive(true);
            // Lấy Rigidbody2D của đạn để áp dụng lực
            Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();

            // Áp dụng lực cho viên đạn để nó di chuyển theo hướng firePoint
            rb.AddForce(firePoint.right * ArrowSpeed, ForceMode2D.Impulse);
            Destroy(arrow, 1);
            ArrowCoolDownSkill = SetTimeArrowSkill;
        }
    }
    public virtual void MagicShoot( )
    {
        if (MagicCoolDownSkill < 0)
        {
            
            GameObject magicSkill = Instantiate(MagicSkill, firePoint.position, firePoint.rotation);
            magicSkill.gameObject.SetActive(true);
            // Lấy Rigidbody2D của đạn để áp dụng lực
            Rigidbody2D rb = magicSkill.GetComponent<Rigidbody2D>();

            // Áp dụng lực cho viên đạn để nó di chuyển theo hướng firePoint
            rb.AddForce(firePoint.right * MagicSpeed, ForceMode2D.Impulse);
            Destroy(magicSkill, 1);
            MagicCoolDownSkill = SetTimeMagicSkill;
        }
    }
}
