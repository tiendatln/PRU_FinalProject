using UnityEngine;

public class DragonAnimation : MonoBehaviour
{
    public Animator animator;
    private Dragon boss;
    private Rigidbody2D rb;


    [Header("Name Animation")]
    private string _AttackName;
    public string _Walk;
    public string _Dead;
    public string _heal;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        boss = GetComponent<Dragon>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Walk()
    {
        animator.SetFloat(_Walk, 2f);
        animator.SetBool("Dash", false);
        animator.SetBool("PrepDash", false);
        boss.isMove = true;
    }

    public void Idle()
    {
        animator.SetFloat(_Walk, 0f);
    }

    public void Dead()
    {
        animator.SetBool(_Dead, true);
    }

    public void Attack(string name)
    {
        boss.isMove = false;
        Idle();
        animator.SetBool(name, true);
        _AttackName = name;
    }

    public void StopAttack()
    {
        boss.ChooseNextAttack();
        boss.isMove = true;
        animator.SetBool(_AttackName, false);
    }


    public void AttackMap()
    {
        animator.SetBool("AttackDown", true);
        boss.isMove = false;
    }

    public void StopAttackMap()
    {
        animator.SetBool("AttackDown", false);
        boss.isMove = true;
    }

    public void PreDash()
    {
        animator.SetBool("PrepDash", true);
        Idle();
        boss.isMove = false;
    }


    public void playDash()
    {
        animator.SetBool("Dash", true);
        Dash();
    }


    public void Dash()
    {

        
        if(boss.IsFacingRight == true )
        {
            rb.AddForce(Vector2.right * 5000, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(Vector2.left * 5000, ForceMode2D.Impulse);
        }
        boss.checkWall();
    }

    public void StopDash()
    {
        boss.isMove = true;
        boss.ChooseNextAttack();
        animator.SetBool("Dash", false);
        animator.SetBool("PrepDash", false);
        

    }

    public void DrinkPotion()
    {
        boss.isMove = false;
        animator.SetBool(_heal, true);
    }

    public void StopDrinkPotion()
    {
        boss.isMove = true;
        animator.SetBool(_heal, false);
    }
}
