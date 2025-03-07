using UnityEngine;

public class BossAnimation : MonoBehaviour
{
    public Animator animator;
    

    [Header("Name Animation")]
    public string AttackName;
    public string _Walk;
    public string _Dead;
    public string _TakeHit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Walk()
    {
        animator.SetFloat(_Walk, 2f);
    }
    public void Dead()
    {
        animator.SetBool(_Dead, true);
    }
    public void TakeHit()
    {
        animator.SetBool(_TakeHit, true);
    }
    public void Attack(string name)
    {
        animator.SetBool(name, true);
        AttackName = name;
    }

    public void StopTakeHit()
    {
        animator.SetBool(_TakeHit, false);
    }
    public void StopAttack()
    {
        animator.SetBool(AttackName, false);
    }
   
    public void Dash()
    {
        animator.SetBool("Dash",true);
    }
    public void StopDash()
    {
        animator.SetBool("Dash", false);
    }

    public void DrinkPotion()
    {
        animator.SetBool("DrinkPotion", true);
    }

    public void StopDrinkPotion()
    {
        animator.SetBool("DrinkPotion", false);
    }
}
