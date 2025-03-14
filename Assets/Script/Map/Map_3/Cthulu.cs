﻿using UnityEngine;
using UnityEngine.UI;

public class Cthulu : MonoBehaviour
{
    private Transform player; // Tham chiếu đến transform của người chơi
    public float moveSpeed = 3f; // Tốc độ di chuyển của boss
    public float maxHealth = 200f; // Máu tối đa

    public LayerMask PlayerMask;
    public Slider slider;

    // Quản lý hồi máu
    private bool hasHealed50 = false; // Đánh dấu đã hồi máu ở 50% chưa
    private bool hasHealed20 = false; // Đánh dấu đã hồi máu ở 20% chưa
    public float healAmount50 = 30f; // Lượng máu hồi ở 50%
    public float healAmount20 = 15f; // Lượng máu hồi ở 20%

    // Quản lý tấn công
    public float attackCooldown = 0f; // Thời gian chờ giữa các đòn tấn công
    private float nextAttackTime; // Thời điểm có thể tấn công tiếp theo


    // Định nghĩa các đòn tấn công với tầm đánh riêng
    public string[] AttackName;
    private int nextAttack;
    [Header("Attack Range")]
    public Vector2 squareAttackSize; // Kích thước hình vuông tấn công

    public float _damage;

    // Trung tâm của boss
    private Vector3 AttackPoint; // Vị trí trung tâm của boss
    public GameObject AttackPosition;
    public BossAnimation BossAnimation;
    public GameObject Gate;

    // Poision Attack
    public GameObject poisionPrefab; // Prefab of the attack
    public Transform firePoint; // The point where the attack spawns
    public float poisionAttackSpeed = 5f; // Speed of the attack
    public float shootCooldown = 2f; // Cooldown time between shots
    private float nextShootTime = 0f; // Track when the next shot can be fired


    #region Private Value

    private bool isAlive = true;
    private string isAttackName;
    private SpriteRenderer spriteRenderer; // Tham chiếu đến SpriteRenderer
    private bool IsFacingRight = true;
    private float currentHealth; // Máu hiện tại
    private int numbarAttack;
    #endregion

    void Start()
    {
        currentHealth = maxHealth; // Khởi tạo máu
        player = GameObject.FindGameObjectWithTag("Player").transform; // Tìm người chơi qua tag

        ChooseNextAttack(); // Chọn đòn tấn công đầu tiên

        spriteRenderer = GetComponent<SpriteRenderer>();
        AttackPoint = AttackPosition.transform.position;

        // fire point as a position of Cthulu
        firePoint = transform;
    }

    void Update()
    {
        if (isAlive)
        {
            AttackPoint = AttackPosition.transform.position;
            if (spriteRenderer != null && spriteRenderer.sprite != null)
            {
                squareAttackSize = spriteRenderer.sprite.bounds.size; // Lấy kích thước thực của sprite
                                                                      // Nếu cần điều chỉnh tỷ lệ, bạn có thể nhân thêm hệ số
                                                                      // squareAttackSize *= 1f; // Ví dụ: giữ nguyên kích thước sprite
            }
            else
            {
                squareAttackSize = new Vector2(2f, 2f); // Giá trị mặc định nếu không tìm thấy sprite

            }

            // Tính khoảng cách từ trung tâm boss đến người chơi trên trục X
            float distanceToPlayer = Mathf.Abs(AttackPoint.x - player.position.x);

            // Xác định tầm đánh hiện tại dựa trên đòn tấn công tiếp theo
            float currentAttackRange = squareAttackSize.x;

            // Di chuyển tới người chơi nếu ngoài tầm tấn công
            if (distanceToPlayer > currentAttackRange)
            {
                MoveTowardsPlayer();
            }
            // Tấn công nếu trong tầm và hết thời gian chờ
            else if (Time.time >= nextAttackTime)
            {
                AttackAnimation();
                nextAttackTime = Time.time + attackCooldown; // Đặt lại thời gian chờ
                ChooseNextAttack(); // Chọn đòn tấn công tiếp theo
            }

            // Quay mặt
            CheckDirectionToFace(player.position.x > transform.position.x);

            // Kiểm tra hồi máu
            CheckHealing();

            // Poision Skill
            ShootPoisionSkill();
        }
        slider.value = currentHealth;
    }

    void ShootPoisionSkill()
    {
        if (Time.time >= nextShootTime)
        {
            // Instantiate the projectile at firePoint's position
            GameObject poisionSkill = Instantiate(poisionPrefab, firePoint.position, Quaternion.identity);

            // Get the Rigidbody2D of the projectile and apply force
            Rigidbody2D rb = poisionSkill.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                float direction = IsFacingRight ? 1f : -1f; // Check boss direction
                rb.linearVelocity = new Vector2(direction * poisionAttackSpeed, 0);
            }

            nextShootTime = Time.time + shootCooldown; // Set next shoot time
        }
    }

    void MoveTowardsPlayer()
    {
        float directionx = Mathf.Sign(player.position.x - transform.position.x);
        Vector3 moveVector = new Vector3(directionx * moveSpeed * Time.deltaTime, 0, 0);
        transform.position += moveVector;
        BossAnimation.Walk();
    }

    #region turn
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
    #endregion

    void ChooseNextAttack()
    {
        // randome đòn đánh tiếp theo

        nextAttack = Random.Range(0, AttackName.Length);

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
            isAlive = false;
            BossAnimation.StopAttack();
            BossAnimation.Dead();
        }
    }

    void Die()
    {
        GameObject gate = Instantiate(Gate, transform.position + new Vector3(0, 0, 10), transform.rotation);
        gate.AddComponent<NextMap>(); // add script chuyển map
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(AttackPoint, new Vector3(squareAttackSize.x, squareAttackSize.y, 0));
    }

    public void SendDamage()
    {
        /*squareAttackSize = spriteRenderer.sprite.bounds.size;*/ // lấy kích thước của animation khi tung chiêu để gửi damge

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
