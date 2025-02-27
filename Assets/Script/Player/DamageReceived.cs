using UnityEngine;

public class DamageReceived : MonoBehaviour
{
    public float health = 100;
    public int attack = 5;
    protected PlayerUI PlayerUI;
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
        PlayerUI = GetComponent<PlayerUI>();
    }

    // Update is called once per frame
    void Update()
    {
        Die();
        PlayerUI.SetHeath(health);
    }

    public virtual void TakeDamage(float _damage)
    {
       health -= _damage;
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
