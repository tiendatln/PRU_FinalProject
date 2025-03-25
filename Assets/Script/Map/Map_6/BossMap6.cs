
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossMap6 : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Mảng chứa nhiều loại quái
    public Transform spawnPoint; // Vị trí spawn quái

    public Transform player;
    public float moveSpeed = 3f;
    public float maxHealth = 200f;

    public Slider slider;
    public LayerMask PlayerMask;


    private bool hasHealed50 = false;
    private bool hasHealed20 = false;
    public float healAmount50 = 30f;
    public float healAmount20 = 15f;

    public float attackCooldown = 0f;
    private float nextAttackTime;

    public string[] AttackName;
    private int nextAttack;

    public Vector2 squareAttackSize;
    public float _damage;

    private Vector3 AttackPoint;
    public GameObject AttackPosition;
    public BossMap6Animation BossAnimation;
    public GameObject Gate;

    private bool isMove = true;
    private bool IsFacingRight = true;
    private float currentHealth;
    private SpriteRenderer spriteRenderer;
    private SpawnSkillMap6 SpawnSkillBoss;
    private float isSpawning;


    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ChooseNextAttack();
        spriteRenderer = GetComponent<SpriteRenderer>();
        AttackPoint = AttackPosition.transform.position;
        SpawnSkillBoss = GameObject.Find("AttackPointMap6").GetComponent<SpawnSkillMap6>(); 
    }

    void Update()
    {
        
        if (isMove)
        {
            AttackPoint = AttackPosition.transform.position;
            float distanceToPlayer = Mathf.Abs(AttackPoint.x - player.position.x);
            float currentAttackRange = squareAttackSize.x;

            if (distanceToPlayer > currentAttackRange)
            {
                MoveTowardsPlayer();
            }
            else if (Time.time >= nextAttackTime && isSpawning > Time.time)
            {
                AttackAnimation();
                nextAttackTime = Time.time + attackCooldown;
            }
            else
            {
                BossAnimation.Idle();
            }
            if (Time.time >= isSpawning)
            {
                BossAnimation.CallEnemy();
                isSpawning = Time.time + 50;
            }
            

            CheckDirectionToFace(player.position.x > transform.position.x);
            CheckHealing();
        }
        slider.value = currentHealth;
    }

    void MoveTowardsPlayer()
    {
        float directionx = Mathf.Sign(player.position.x - transform.position.x);
        transform.position += new Vector3(directionx * moveSpeed * Time.deltaTime, 0, 0);
        BossAnimation.Walk();
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

    public void ChooseNextAttack()
    {
        int compareAttack = nextAttack;
        do
        {
            nextAttack = Random.Range(0, AttackName.Length);

        } while (compareAttack == nextAttack);
        
    }

    void AttackAnimation()
    {
        if (Mathf.Abs(AttackPoint.x - player.position.x) <= squareAttackSize.x)
        {
            BossAnimation.Attack(AttackName[nextAttack]);
        }
    }

  

    void CheckHealing()
    {
        float healthPercentage = (currentHealth / maxHealth) * maxHealth;
        if (healthPercentage <= (maxHealth / 2) && !hasHealed50)
        {
            BossAnimation.DrinkPotion();
        }
        else if (healthPercentage <= (maxHealth / 3) && !hasHealed20)
        {
            BossAnimation.DrinkPotion();
        }
    }

    public void Heal(float amount)
    {
        float healthPercentage = (currentHealth / maxHealth) * 200f;
        float heal = 0;
        if (healthPercentage <= 100f && !hasHealed50)
        {
            heal = healAmount50;
            hasHealed50 = true;
        }
        else if (healthPercentage <= 50f && !hasHealed20)
        {
            heal = healAmount20;
            hasHealed20 = true;
        }
        currentHealth = Mathf.Min(currentHealth + heal, maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            isMove = false;
            BossAnimation.StopAttack();
            BossAnimation.Dead();
        }
    }

    void Die()
    {
        GameObject gate = Instantiate(Gate, transform.position + new Vector3(0, 0, 10), transform.rotation);
        gate.AddComponent<NextMap>();
        Destroy(gameObject);
    }

    public void Shoot()
    {
        SpawnSkillBoss.Shoot();
    }
    public void CallEnemy()
    {
        if (enemyPrefabs.Length == 0) return; // Kiểm tra nếu không có Prefab nào tránh lỗi

        // Chọn ngẫu nhiên 1 quái trong danh sách enemyPrefabs
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemyPrefab = enemyPrefabs[randomIndex];

        int ran = Random.Range(-90, 90);
        GameObject enemy = Instantiate(enemyPrefab, AttackPoint + new Vector3(3,0,0), enemyPrefab.transform.rotation);
        
  

    }


    public void SendDamage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(AttackPoint, squareAttackSize, PlayerMask);
        foreach (Collider2D enemy in hitEnemies)
        {

            // Kiểm tra xem enemy có component EnemyAI_2D hay không
            if (enemy.gameObject.TryGetComponent<DamageReceived>(out DamageReceived player))
            {
                player.TakeDamage(_damage); // Gây sát thương lên kẻ địch
            }

        }
    }
}
