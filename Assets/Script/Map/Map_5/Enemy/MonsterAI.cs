using System.Collections;
using UnityEngine;

public class EnemyAI_2D : MonoBehaviour
{
    public Transform pointA, pointB; // Hai điểm tuần tra
    public float speed = 2f; // Tốc độ di chuyển
    public float detectionRange = 5f; // Phạm vi phát hiện Player
    public float attackRange = 1f; // Phạm vi tấn công
    public float attackCooldown = 1f; // Thời gian chờ giữa các lần tấn công
    public float MaxHP;
    private Transform player;
    private Vector2 nextPoint;
    //private bool isChasing = false;
    private bool canAttack = true;
    private bool IsFacingRight = true; // Kiểm tra hướng của Enemy
    public Animator animator;
    public Rigidbody2D rb;
    private float HP;
    [Header("Check Attack")]
    public Transform attackPoint; // Điểm tấn công
    public float _attackRange = 5f;
    public LayerMask enemyLayer;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        nextPoint = pointB.position; // Điểm tuần tra đầu tiên
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        HP = MaxHP;
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        Debug.Log(HP);
        if (HP <= 0)
        {
            
            animator.SetBool("Death", true);
            Invoke("Dead", 2.17f);
        }
        else
        {
            if (distanceToPlayer <= attackRange)
            {
                //if()
                Attack();
            }
            else if (distanceToPlayer <= detectionRange)
            {
                ChasePlayer();
            }
            else
            {
                #region Animation Walk
                animator.SetFloat("Walk", 2f);
                Invoke("Patrol", 0.33f);
                #endregion
            }
        }
    }

    #region Stop Animation
    public void StopAttack()
    {
        animator.SetBool("Cleave",false); // Ghi ten parameter cua Attack
    }
    #endregion

    #region Move to check point
    void Patrol()
    {
        //isChasing = false;
        transform.position = Vector2.MoveTowards(transform.position, nextPoint, speed * Time.deltaTime);

        // Cập nhật hướng quay mặt khi quay lại tuần tra
        CheckDirectionToFace(nextPoint.x > transform.position.x);

        // Kiểm tra nếu đến gần điểm tuần tra, đổi hướng
        //if (Vector2.Distance(transform.position, nextPoint) < 0.1f)
        //{
            
        //    nextPoint = (nextPoint == (Vector2)pointA.position) ? pointB.position : pointA.position;
        //}
    }

    #endregion


    void ChasePlayer()
    {
        //isChasing = true;
        animator.SetFloat("Walk", 2f);
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        CheckDirectionToFace(player.position.x > transform.position.x); // Lật mặt theo hướng Player
    }

    void Attack()
    {
        if (canAttack)
        {
            Debug.Log("Enemy Attacks!");
            #region Tat Animation walk va bat Attack 
            animator.SetBool("Cleave", true);
            animator.SetFloat("Walk", 0f);
            Invoke("SeandDamage", 0.9f);
            Invoke("StopAttack", 1.495f);
            #endregion
            canAttack = false;
            StartCoroutine(ResetAttack());
        }
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    //void Flip(bool shouldFaceRight)
    //{
    //    if (shouldFaceRight != facingRight) // Nếu hướng hiện tại không đúng, thì lật lại
    //    {
    //        facingRight = shouldFaceRight;
    //        Vector3 localScale = transform.localScale;
    //        localScale.x *= -1; // Lật mặt theo trục X
    //        transform.localScale = localScale;
    //    }
    //}
    #region Turn
    private void Turn()
    {
        //stores scale and flips the player along the x axis, 
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.y -= -180;
        transform.rotation = Quaternion.Euler(rotation);

        IsFacingRight = !IsFacingRight;
    }
    public void CheckDirectionToFace(bool isMovingRight)
    {
        if (isMovingRight != IsFacingRight)
            Turn();
    }
    #endregion

    public void TakeDamage(float damage)
    {
        
        HP -= damage;
        if (HP > 0) { 
        animator.SetBool("Take Hit", true);
        Invoke("StopTakeDamage", 0.49f);
        }
    }
    IEnumerator DisableControlTemporarily(float time)
    {
        rb.bodyType = RigidbodyType2D.Static;
        animator.SetFloat("Walk", 0);
        animator.SetBool("jump", false);
        yield return new WaitForSeconds(time); // Chờ một khoảng thời gian
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
    void StopTakeDamage()
    {
        animator.SetBool("Take Hit", false);
    }
    void Dead()
    {
        Destroy(this.gameObject);
    }

    void SeandDamage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            float at = 10f; // Sát thương tính theo phần trăm

            // Kiểm tra xem enemy có component EnemyAI_2D hay không
            if (enemy.gameObject.TryGetComponent<DamageReceived>(out DamageReceived player))
            {
                player.TakeDamage(at); // Gây sát thương lên kẻ địch
            }

            Debug.Log("Hit " + enemy.name);
        }
    }
}