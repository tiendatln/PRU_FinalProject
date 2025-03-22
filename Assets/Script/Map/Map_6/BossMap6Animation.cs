using UnityEngine;

public class BossMap6Animation : MonoBehaviour
{
    public Animator animator;
    private BossMap6 boss;

    [Header("Name Animation")]
    private string _AttackName;
    public string _Walk;
    public string _Dead;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        boss = GetComponent<BossMap6>();
    }

    public void Walk()
    {
        animator.SetFloat(_Walk, 2f);
    }

    public void Idle()
    {
        animator.SetFloat(_Walk, 0);
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
        animator.SetBool("DrinkPotion", true);
    }

    public void StopDrinkPotion()
    {
        animator.SetBool("DrinkPotion", false);
    }

    public void CallEnemy()
    {
        animator.SetBool("isAttack3", true);
    }

    public void StopCallEnemy()
    {
        animator.SetBool("isAttack3", false);
    }
}
