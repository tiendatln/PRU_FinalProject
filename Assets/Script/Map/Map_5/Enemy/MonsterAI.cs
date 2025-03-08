using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI_2D : MonoBehaviour
{
    public float speed = 2f; // Movement speed
    public float patrolDistance = 5f; // Distance to patrol from starting position
    public float detectionRange = 5f; // Player detection range
    public float attackRange = 1f; // Attack range
    public float attackCooldown = 1f; // Cooldown between attacks
    public float MaxHP;
    private Transform player;
    private Vector2 startPosition; // Starting position of enemy
    private Vector2 targetPosition; // Current target position for patrolling
    public int EX;
    private bool canAttack = true;
    private bool IsFacingRight = true;
    public Animator animator;
    public Rigidbody2D rb;
    private float HP;

    [Header("Check Attack")]
    public Transform attackPoint;
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

    public GameObject HpSlider;
    public Slider Slider;
    [HideInInspector] public bool isAttack = true;

    public MonsterSpawnSKill monsterSpawnSkill;
    public PlayerController playerController;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        startPosition = transform.position; // Store initial position
        
        targetPosition = startPosition + Vector2.right * patrolDistance; // Initial target
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Slider = HpSlider.GetComponent<Slider>();
        playerController = player.GetComponent<PlayerController>();
        HP = MaxHP;
    }

    void Update()
    {
        if (player == null) return;
        
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
                if (canAttack)
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
                Patrol();
            }
        }
    }

    void Patrol()
    {
        animator.SetFloat(WalkAnimationName, 2f);

        // Move towards target position
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Check direction
        CheckDirectionToFace(targetPosition.x > transform.position.x);

        // If reached target position, switch direction
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            if (targetPosition.x > startPosition.x)
                targetPosition = startPosition - Vector2.right * patrolDistance; // Move left
            else
                targetPosition = startPosition + Vector2.right * patrolDistance; // Move right
        }
    }

    void ChasePlayer()
    {
        animator.SetFloat(WalkAnimationName, 2f);
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        CheckDirectionToFace(player.position.x > transform.position.x);
    }

    void Attack()
    {
        CheckDirectionToFace(player.position.x > transform.position.x);
        animator.SetBool(AttackAnimationName, true);
        animator.SetFloat(WalkAnimationName, 2f);
        
    }

    
    public void StopAttack()
    {
        animator.SetFloat(WalkAnimationName, 0f);
        animator.SetBool(AttackAnimationName, false);
        canAttack = true;
    }

    private void Turn()
    {
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

    public void TakeDamage(float damage)
    {
        HP -= damage;
        Slider.value = HP;
        if (HP > 0)
        {
            spriteRenderer.color = Color.red;
            Invoke("StopTakeDamage", 0.2f);
        }
    }

    void StopTakeDamage()
    {
        spriteRenderer.color = Color.white;
    }

    void Dead()
    {
        playerController.PlayerLever.TakeLever(EX);
        Destroy(this.gameObject);
    }

    public void SeandDamage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.gameObject.TryGetComponent<DamageReceived>(out DamageReceived player))
            {
                player.TakeDamage(AttackDamage);
            }
        }
    }

    public void Shoot()
    {
        monsterSpawnSkill.Shoot();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.blue;
        if (Application.isPlaying)
        {
            Gizmos.DrawLine(startPosition - Vector2.right * patrolDistance, startPosition + Vector2.right * patrolDistance);
        }
    }
}