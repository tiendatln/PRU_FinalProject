using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public Animator animator;
    private int AttackCount = 0;
    private int MaxAttack = 3;
    public Rigidbody2D rb;
    protected PlMove PlMove;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        PlMove = GetComponent<PlMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (AttackCount < MaxAttack)
            {
                AttackCount++;
                Animation();
            }
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            animator.SetBool("Bow Attack", true);
            PlMove.CanMove(false);
            Invoke("EndBowAttack", 1f);
        }
    }

    #region Animation
    void Animation()
    {
        PlMove.CanMove(false);
        PlMove.StopJump(0);
        if (AttackCount == 1)
        {
            animator.SetBool("Attack01", true);
            Invoke("EndAttack01", 0.4f);
        }
        else if (AttackCount == 2)
        {
            animator.SetBool("Attack02", true);
            Invoke("EndAttack02", 0.6f);
        }
        else if (AttackCount == 3)
        {
            animator.SetBool("Attack03", true);
            Invoke("EndAttack03", 0.9f);
        }
        
    }
    #endregion


    public void EndAttack01()
    {
        animator.SetBool("Attack01", false);
        if (AttackCount == 1)
        {
            AttackCount = 0;
            animator.SetInteger("Next Attack", 0);
            PlMove.CanMove(true);
            PlMove.StopJump(2);
        }
        else
        {
            animator.SetInteger("Next Attack", 2);
            
        }

    }

    public void EndAttack02()
    {
        animator.SetBool("Attack02", false);
        if (AttackCount == 2)
        {
            AttackCount = 0;
            animator.SetInteger("Next Attack", 0);
            PlMove.CanMove(true);
            PlMove.StopJump(2);
        }
        else
        {
            animator.SetInteger("Next Attack", 3);
            
        }

    }


    public void EndAttack03()
    {
        animator.SetBool("Attack03", false);
        if (AttackCount == 3)
        {
            AttackCount = 0;
            animator.SetInteger("Next Attack", 0);
        }
        PlMove.CanMove(true);
        PlMove.StopJump(2);
    }

    public void EndBowAttack()
    {
        animator.SetBool("Bow Attack", false);
        PlMove.CanMove(true);
        PlMove.StopJump(2);
    }

}
