
using UnityEngine;

public class FireBird : MonoBehaviour
{

    public LayerMask Ground;
    public float _Damage;
    public float rotationSpeed = 5f;
    private Transform player;
    public float SkillSpeed;

    private Rigidbody2D rb;

    private void Start()
    {
        player = GameObject.Find("Character").transform;
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Update()
    {

        if (player != null)
        {
            // Tính hướng quay
            Vector3 direction = (player.position - transform.position).normalized;

            // Chuyển hướng thành góc quay cho 2D
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Áp dụng góc quay theo trục Z thay vì sử dụng LookRotation
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

            // Quay dần về hướng Player 
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            if (rb != null)
            {
                rb.AddForce(transform.right * SkillSpeed, ForceMode2D.Force);
            }
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
     
        if (collision.gameObject.TryGetComponent<DamageReceived>(out DamageReceived player))
        {
            player.TakeDamage(_Damage);

            //Invoke("StopAnimation", 0.35f);
            this.gameObject.SetActive(false);

        }

    }

    public void stopAnimation()
    {
        this.gameObject.SetActive(false);
    }
}
