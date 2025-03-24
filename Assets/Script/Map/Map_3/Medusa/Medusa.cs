using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class Medusa : MonoBehaviour
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



    [Header("Name Animation")]
    public string WalkAnimationName;
    public string RunAnimationName;
    public string DeathAnimationName;
    public string[] AttackNames;
    private int nextAttack;

    // Hp slider
    public GameObject HpSlider;
    private Slider Slider;
    [HideInInspector] public bool isAttack = true;

    // player controller
    private PlayerController playerController;
    private SpriteRenderer spriteRenderer;

    // Heal Hp
    [Header("HP Prefab")]
    public AssetLabelReference _HeathPlayer;
    private AsyncOperationHandle<GameObject> Handle;

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

        // random animation attack dau tien
        ChooseNextAttack();
    }

    void Update()
    {
        attackCooldown -= Time.deltaTime;

        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(attackPoint.position, player.position);
        AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);

        if (HP <= 0)
        {
            animator.SetBool(DeathAnimationName, true);
        }
        else
        {
            if (distanceToPlayer <= attackRange)
            {
                if (canAttack && attackCooldown <= 0)
                {
                    canAttack = false;
                    Attack();
                    attackCooldown = 2f; // reset attack cooldown
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
        if (animator.GetFloat(WalkAnimationName) == 0)
        {
            animator.SetFloat(WalkAnimationName, 2f);
        }

        animator.SetBool("isWalk", true);
        animator.SetBool("isRun", false);

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
        animator.SetBool("isWalk", false);
        animator.SetBool("isRun", true);

        if (animator.GetBool(RunAnimationName) == false)
        {
            animator.SetBool(RunAnimationName, true);
        }

        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        CheckDirectionToFace(player.position.x > transform.position.x);
    }

    // Random attack animation
    void ChooseNextAttack()
    {
        // randome đòn đánh tiếp theo
        nextAttack = Random.Range(0, AttackNames.Length);
    }

    void Attack()
    {
        CheckDirectionToFace(player.position.x > transform.position.x);
        animator.SetBool(AttackNames[nextAttack], true);

    }

    public void StopAttack()
    {
        animator.SetBool(AttackNames[nextAttack], false);
        canAttack = true;

        // random new attack
        ChooseNextAttack();
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
        Handle = Addressables.LoadAssetAsync<GameObject>(_HeathPlayer.labelString);
        for (int i = 0; i < 3; i++)
        {
            SpawnHeath();
        }
        this.gameObject.SetActive(false);
    }

    private void SpawnHeath()
    {
        if (string.IsNullOrEmpty(_HeathPlayer.labelString))
        {
            return;
        }

        Handle.Completed += (AsyncOperationHandle<GameObject> task) =>
        {
            GameObject heathPlayer = UnityEngine.Object.Instantiate(task.Result); // Tạo instance từ asset đã tải
            heathPlayer.transform.position = this.transform.position + new Vector3(0, 1, 0);

        };
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
