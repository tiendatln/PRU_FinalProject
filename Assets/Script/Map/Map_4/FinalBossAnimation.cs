using UnityEngine;

public class FinalBossAnimation : MonoBehaviour
{
    public Animator animator;
    private FinalBoss boss;


    [Header("Name Animation")]
    private string _AttackName;
    public string _Walk;
    public string _Dead;
    public string _heal;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();    
        boss = GetComponent<FinalBoss>();
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
        boss.ChooseNextAttack();
        
        animator.SetBool(_AttackName, false);
    }

    public void Dash()
    {
        animator.SetBool("Dash", true);
    }

    public void StopDash()
    {
        animator.SetBool("Dash", false);
    }

    public void DrinkPotion()
    {
        
        animator.SetBool(_heal, true);
    }

    public void StopDrinkPotion()
    {
        
        animator.SetBool(_heal, false);
    }   
}
