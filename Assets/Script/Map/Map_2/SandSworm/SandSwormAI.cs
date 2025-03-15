using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SandSwormAI : MonoBehaviour
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

private float HP;

[Header("Check Attack")]
public Transform attackPoint;

public LayerMask enemyLayer;
public float AttackDamage;


public LayerMask groundLayer;
[Header("Time Animation")]

private float TimeStopAnimation;

[Header("Name Animation")]
public bool isFly;
public string WalkAnimationName;
public string AttackAnimationName;
public string DeathAnimationName;

public GameObject HpSlider;
public Slider Slider;
[HideInInspector] public bool isAttack = true;

public MonsterSpawnSKill monsterSpawnSkill;
public PlayerController playerController;
private SpriteRenderer spriteRenderer;

private bool resetLine = false;

[HideInInspector] public float distanceToPlayer;
void Start()
{
    player = GameObject.FindGameObjectWithTag("Player")?.transform;
    startPosition = transform.position; // Store initial position

    targetPosition = startPosition + Vector2.right * patrolDistance; // Initial target
    spriteRenderer = GetComponent<SpriteRenderer>();
    animator = GetComponent<Animator>();

    Slider = HpSlider.GetComponent<Slider>();
    playerController = player.GetComponent<PlayerController>();
    Slider.maxValue = MaxHP;
    Slider.value = MaxHP;
    HP = MaxHP;

}

void Update()
{
    if (player == null) return;

    distanceToPlayer = Vector2.Distance(attackPoint.position, player.position);

    AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);
    TimeStopAnimation = animationState.length;
    if (HP <= 0)
    {
        animator.SetBool(DeathAnimationName, true);
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


            resetLine = true;
        }
        else if (!isFly && distanceToPlayer >= detectionRange && resetLine)
        {
            Vector2 newLine = new Vector2(transform.position.x, transform.position.y);
            targetPosition = newLine + Vector2.right * patrolDistance;
            resetLine = false;
        }
        else
        {
                
        }
    }
}


void Patrol()
{
    //if (!isFly)
    //{
    //    if (animator.GetFloat(WalkAnimationName) == 0)
    //    {
    //        animator.SetFloat(WalkAnimationName, 2f);
    //    }
    //}


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

    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    CheckDirectionToFace(player.position.x > transform.position.x);
}

void Attack()
{
    CheckDirectionToFace(player.position.x > transform.position.x);
    animator.SetBool(AttackAnimationName, true);
    if (!isFly)
    {
        if (animator.GetFloat(WalkAnimationName) == 2)
        {
            animator.SetFloat(WalkAnimationName, 0f);
        }
    }
}


public void StopAttack()
{
    animator.SetBool(AttackAnimationName, false);
    if (!isFly)
    {
        if (animator.GetFloat(WalkAnimationName) == 0)
        {
            animator.SetFloat(WalkAnimationName, 2f);
        }
    }
    canAttack = true;
}

private void Turn()
{
    Vector3 scale = transform.localScale;
    scale.x *= -1;
    transform.localScale = scale;

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
    canAttack = false;
    Slider.value = HP;
    if (HP > 0)
    {
        spriteRenderer.color = Color.red;
        Invoke("StopTakeDamage", 0.2f);
    }
}

void StopTakeDamage()
{
    canAttack = true;
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
    Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    Gizmos.color = Color.green;
    Gizmos.DrawWireSphere(transform.position, detectionRange);
    Gizmos.color = Color.blue;
    if (Application.isPlaying)
    {
        Gizmos.DrawLine(startPosition - Vector2.right * patrolDistance, startPosition + Vector2.right * patrolDistance);
    }
}
}
