using UnityEngine;

public class DamageReceived : MonoBehaviour
{
    public int health = 100;
    public int attack = 5;
    private Rigidbody2D rb;
    private Animator animator;
    private CapsuleCollider2D capsuleCollider;
    private Vector2 capsuleColliderSize = new Vector2(0.31f, 0.13f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Die();
    }

    public virtual void TakeDamage(float Face)
    {
        health -= attack;
        rb.AddForce((4f * -Face) * Vector2.left, ForceMode2D.Impulse);
        rb.AddForce(2f  * Vector2.up, ForceMode2D.Impulse);
        animator.SetBool("Damage Recived", true);
        Invoke("StopReceived", 0.11f);
        capsuleCollider.enabled = false;
    }

    public virtual void SendDamage()
    {

    }

    void Die()
    {
        if (health == 0)
        {
            animator.SetBool("Damage Recived", true);
        }
    }

    void StopReceived()
    {
        capsuleCollider.enabled = true;
        animator.SetBool("Damage Recived", false);
    }
}
