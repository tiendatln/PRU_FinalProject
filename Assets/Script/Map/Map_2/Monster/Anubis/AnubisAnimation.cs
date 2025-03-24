using UnityEngine;

public class AnubisAnimation : MonoBehaviour
{
    public Animator animator;
    private AnubisAI boss;


    [Header("Name Animation")]
    private string _AttackName;
    public string _Walk;
    public string _Dead;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boss = GetComponent<AnubisAI>();
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
        boss.isMove = false;
    }

    public void StopAttack()
    {
        animator.SetBool(_AttackName, false);
        boss.ChooseNextAttack();
        boss.isMove = true;
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
        animator.SetBool("IsAttack4", true);
    }

    public void StopDrinkPotion()
    {
        animator.SetBool("IsAttack4", false);
    }
}
