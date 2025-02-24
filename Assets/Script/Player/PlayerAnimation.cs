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


    public virtual void NomalMagicSkill()
    {
        animator.SetBool("jump", false);
        animator.SetBool("Skill", true);
        animator.SetBool("fallen", false);
        
    }
    public void Fallen(bool active)
    {
        animator.SetBool("jump", false);
        animator.SetBool("fallen", active);
    }
    public void EndMagicSkill()
    {
        animator.SetBool("Skill", false);
    }

    public void BowAttack()
    {
        animator.SetBool("jump", false);
        animator.SetBool("fallen", false);
        animator.SetBool("Bow Attack", true);
        
    }
    public void EndBowAttack()
    {
        animator.SetBool("Bow Attack", false);
        
    }
}
