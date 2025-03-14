using UnityEngine;

public class BossAnimation : MonoBehaviour
{
    public Animator animator;
    

    [Header("Name Animation")]
    private string _AttackName;
    public string _Walk;
    public string _Dead;


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
    
    public void Attack(string name)
    {
        animator.SetBool(name, true);
        _AttackName = name;
    }
    
    public void StopAttack()
    {
        animator.SetBool(_AttackName, false);
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
