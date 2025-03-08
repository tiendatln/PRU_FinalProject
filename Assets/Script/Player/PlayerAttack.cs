using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{


    public Animator animator;
    private int AttackCount = 0;
    private int MaxAttack = 3;
    public Rigidbody2D rb;
    protected PlMove PlMove;

    [SerializeField] private Vector2 _groundCheckSize = new Vector2(0.23f, 0.3f);
    public float attackRange = 1.5f; // Tầm đánh của nhân vật
    public LayerMask enemyLayer; // Lớp kẻ địch
    public Transform attackPoint; // Điểm tấn công
   
    protected PlayerController playerController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        PlMove = GetComponent<PlMove>();
       
        playerController = GetComponent<PlayerController>();
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
        
    }

    #region Animation
    void Animation()
    {
        
        animator.SetBool("jump", false);
        if (AttackCount == 1)
        {
            
            animator.SetBool("Attack01", true);
            
            Invoke("EndAttack01", 0.18f);
            Invoke("Attack", 0.04f);
            
        }
        else if (AttackCount == 2)
        {
            
            animator.SetBool("Attack02", true);
            
            Invoke("EndAttack02", 0.6f);
            Invoke("Attack", 0.5f);
            
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
        
        if (AttackCount == 3)
        {
            AttackCount = 0;
            animator.SetInteger("Next Attack", 0);
        }
        
    }

    

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {

            // Kiểm tra xem enemy có component EnemyAI_2D hay không
            if (enemy.gameObject.TryGetComponent<EnemyAI_2D>(out EnemyAI_2D _enemy))
            {
                _enemy.TakeDamage(playerController.PlayerMainData.attack); // Gây sát thương lên kẻ địch
            }
            if (enemy.gameObject.TryGetComponent<Boss>(out Boss boss))
            {
                boss.TakeDamage(playerController.PlayerMainData.attack);
            }

            Debug.Log("Hit " + enemy.name);
        }
    }

}
