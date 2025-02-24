using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{



    public int HP = 100;
    public GameObject ememy;

    public Animator animator;
    private int AttackCount = 0;
    private int MaxAttack = 3;
    public Rigidbody2D rb;
    protected PlMove PlMove;

    [SerializeField] private Vector2 _groundCheckSize = new Vector2(0.23f, 0.3f);
    public float attackRange = 1.5f; // Tầm đánh của nhân vật
    public LayerMask enemyLayer; // Lớp kẻ địch
    public Transform attackPoint; // Điểm tấn công
    protected Enemy Enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        PlMove = GetComponent<PlMove>();
        Enemy = GetComponent<Enemy>();
        ememy = GameObject.Find("HP");
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
            PlMove.CanMove(0);
            Invoke("EndBowAttack", 1f);
        }
    }

    #region Animation
    void Animation()
    {
        PlMove.CanMove(0);
        animator.SetBool("jump", false);
        if (AttackCount == 1)
        {
            animator.SetBool("Attack01", true);
            Invoke("EndAttack01", 0.4f);
            Invoke("Attack", 0.26f);
            Attack();
        }
        else if (AttackCount == 2)
        {
            
            animator.SetBool("Attack02", true);
            Invoke("EndAttack02", 0.6f);
            Invoke("Attack", 0.5f);
            Attack();
        }
        else if (AttackCount == 3)
        {
            
            animator.SetBool("Attack03", true);
            Invoke("EndAttack03", 0.9f);
            Invoke("Attack", 0.8f);
            
        }
        
    }
    #endregion


    public void EndAttack01()
    {
        animator.SetBool("Attack01", false);
        PlMove.CanMove(1);
        if (AttackCount == 1)
        {
            AttackCount = 0;
            animator.SetInteger("Next Attack", 0);
            
        }
        else
        {
            animator.SetInteger("Next Attack", 2);
            
        }

    }

    public void EndAttack02()
    {
        animator.SetBool("Attack02", false);
        PlMove.CanMove(1);
        if (AttackCount == 2)
        {
            AttackCount = 0;
            animator.SetInteger("Next Attack", 0);
            
        }
        else
        {
            animator.SetInteger("Next Attack", 3);
            
        }

    }


    public void EndAttack03()
    {
        animator.SetBool("Attack03", false);
        PlMove.CanMove(1);
        if (AttackCount == 3)
        {
            AttackCount = 0;
            animator.SetInteger("Next Attack", 0);
        }
        
    }

    public void EndBowAttack()
    {
        animator.SetBool("Bow Attack", false);
        PlMove.CanMove(1);
        PlMove.StopJump(2);
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            HP -= 5;
            float at = (5f / 100f) * 2; // Sửa lại để phép chia chính xác

            // Kiểm tra gameObject có được gán chưa
            if (ememy != null)
            {
                // Điều chỉnh kích thước gameObject
                ememy.transform.localScale = new Vector2(ememy.transform.localScale.x - at, ememy.transform.localScale.y);
            }
            Debug.Log(HP);
            Debug.Log("Hit " + enemy.name);
            // Gọi hàm gây sát thương hoặc xử lý khác ở đây
        }
    }

}
