using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    public Animator animator;
    public Rigidbody2D rb;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rb.linearVelocityY < 0)
        {
            animator.SetBool("fallen", true);
        }
    }

    public virtual void NomalMagicSkill()
    {
        animator.SetBool("jump", false);
        animator.SetBool("Skill", true);
        animator.SetBool("fallen", false);
        Invoke("EndMagicSkill", 0.2f);
    }
    public void EndMagicSkill()
    {
        animator.SetBool("Skill", false);
    }

}
