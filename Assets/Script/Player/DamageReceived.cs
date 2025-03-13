using UnityEngine;

public class DamageReceived : MonoBehaviour
{
    
   
    private Rigidbody2D rb;
    private Animator animator;
    private CapsuleCollider2D capsuleCollider;
    private Vector2 capsuleColliderSize = new Vector2(0.31f, 0.13f);
    public CapsuleCollider2D CapsuleCollider2D;
    public PlayerController controller;
    
    private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        CapsuleCollider2D = GetComponent<CapsuleCollider2D>();
        controller = GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Die();

        UIController.Instance.GetPlayerUI().GetComponent<PlayerUI>().SetHeath(controller.PlayerMainData.health);

        //controller.PlayerUI.SetHeath(controller.PlayerMainData.health);

    }

    public virtual void TakeDamage(float _damage)
    {
        controller.PlayerMainData.health -= _damage;
        spriteRenderer.color = Color.red;
        Invoke("setDefaulColor", 0.3f);
    }

    public void setDefaulColor()
    {
        spriteRenderer.color = Color.white;
    }
  

    void Die()
    {
        if (controller.PlayerMainData.health <= 0)
        {
            animator.SetBool("Death", true);
            controller.SetCanMove();
            capsuleCollider.isTrigger = true;
            
            Invoke("StopGame", 2f);
        }
    }

    void StopReceived()
    {
        capsuleCollider.enabled = true;
        animator.SetBool("Damage Recived", false);
    }

    void StopGame()
    {
        Time.timeScale = 0f;
        UIController.Instance.GetDeadMenu().SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("enemy"))
        {
            TakeDamage(5);
            if (controller.PlMove.IsFacingRight)
            {
                //rb.AddForce(Vector2.left +);
            }
        }
    }
}
