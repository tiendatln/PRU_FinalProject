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
        
    }

    public virtual void TakeDamage(float Face)
    {
        health -= attack;
        rb.AddForce((20f * Face) * Vector2.left, ForceMode2D.Impulse);
        rb.AddForce(5f  * Vector2.up, ForceMode2D.Impulse);
        animator.SetBool("Damage Recived", true);
        Invoke("StopReceived", 1.3f);
        
    }

    void StopReceived()
    {
        animator.SetBool("Damage Recived", false);
    }
}
