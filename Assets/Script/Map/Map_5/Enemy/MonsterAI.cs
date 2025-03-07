using System.Collections;
using UnityEngine;
using UnityEngine.UI;


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
    public int EX;
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
    public float AttackDamage;

    [Header("Time Animation")]
    
    public float TimeSendDamage;
    private float TimeStopAnimation;

    [Header("Name Animation")]
    public string WalkAnimationName;
    public string AttackAnimationName;
    public string TakeDamageAnimationName;
    public string DeathAnimationName;
    
    public Slider slider;
    [HideInInspector] public bool isAttack = true;

    public MonsterSpawnSKill monsterSpawnSkill;
    public PlayerController playerController;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        nextPoint = pointB.position; // Điểm tuần tra đầu tiên
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        slider = GameObject.Find("Heath Bar Fire Worm")?.GetComponent<Slider>();
        playerController = player.GetComponent<PlayerController>();
        HP = MaxHP;
    }

    void Update()
    {
        if (player == null) return;
        slider.value = HP;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);
        TimeStopAnimation = animationState.length;

        

        if (HP <= 0)
        {
            
            animator.SetBool("Death", true);
            Invoke("Dead", TimeStopAnimation);
        }
        else
        {
            if (distanceToPlayer <= attackRange)
            {
                if (canAttack )
                {
                    
                    canAttack = false;
                    Attack();
                }
            }
            else if (distanceToPlayer <= detectionRange)
            {
                
                ChasePlayer();
            }
            else
            {
                #region Animation Walk
                animator.SetFloat(WalkAnimationName, 2f);
                Patrol();
                #endregion
            }
        }
    }

    #region Stop Animation
    public void StopAttack()
    {
        animator.SetBool(AttackAnimationName,false); // Ghi ten parameter cua Attack
    }
    #endregion

    #region Move to check point
    void Patrol()
    {
        //isChasing = false;

        transform.position = Vector2.MoveTowards(transform.position, nextPoint, speed * Time.deltaTime);

        // Cập nhật hướng quay mặt khi quay lại tuần tra
        CheckDirectionToFace(nextPoint.x > transform.position.x);

        //Kiểm tra nếu đến gần điểm tuần tra, đổi hướng
        if (Vector2.Distance(transform.position, nextPoint) < 0.1f)
        {

            nextPoint = (nextPoint == (Vector2)pointA.position) ? pointB.position : pointA.position;
        }
    }

    #endregion


    void ChasePlayer()
    {
        //isChasing = true;
        animator.SetFloat(WalkAnimationName, 2f);
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        CheckDirectionToFace(player.position.x > transform.position.x); // Lật mặt theo hướng Player
    }

    void Attack()
    {
        CheckDirectionToFace(player.position.x > transform.position.x);
        
            #region Tat Animation walk va bat Attack 
            animator.SetBool(AttackAnimationName, true);
            animator.SetFloat(WalkAnimationName, 2f);
            
            
            
            #endregion
           
            StartCoroutine(ResetAttack());
        
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(2f);
        animator.SetFloat(WalkAnimationName, 0f);
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
        animator.SetBool(TakeDamageAnimationName, true);
        Invoke("StopTakeDamage", TimeStopAnimation);
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
        animator.SetBool(TakeDamageAnimationName, false);
    }
    void Dead()
    {
        playerController.PlayerLever.TakeLever(EX);
        Destroy(this.gameObject);
    }

    void SeandDamage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {

            // Kiểm tra xem enemy có component EnemyAI_2D hay không
            if (enemy.gameObject.TryGetComponent<DamageReceived>(out DamageReceived player))
            {
                player.TakeDamage(AttackDamage); // Gây sát thương lên kẻ địch
            }

        }
    }

    public void Shoot()
    {
        monsterSpawnSkill.Shoot();
    }
    void OnDrawGizmosSelected()
    {
        // Hiển thị tầm đánh từ trung tâm boss
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}