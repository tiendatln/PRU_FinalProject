using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;

public class FinalBoss : MonoBehaviour
{
    public Transform player; // Tham chiếu đến người chơi
    public float moveSpeed = 3f; // Tốc độ di chuyển của boss
    public float maxHealth = 200f; // Máu tối đa

    public Vector2 checkWallSize; // ko dùng
    public Transform checkWallPoint; // ko dùng

    public LayerMask PlayerMask;
    public Slider slider;
    public Animator animator;
    private bool isInvulnerable = false;
    // Trạng thái miễn nhiễm sát thương
    public float invulnerableDuration = 3f; // Thời gian miễn nhiễm
    public string immuneParamName; // Sự kiện khi kết thúc miễn nhiễm
    private float lastInvulnerableTime = 0f; // Thời gian cuối cùng kích hoạt bất tử
    private float invulnerableCooldown = 15f; // Khoảng thời gian giữa mỗi lần bất tử

    // Quản lý hồi máu
    private bool hasHealed50 = false; // Đánh dấu đã hồi máu ở 50% chưa
    private bool hasHealed20 = false; // Đánh dấu đã hồi máu ở 20% chưa
    public float healAmount50 = 30f; // Lượng máu hồi ở 50%
    public float healAmount20 = 15f; // Lượng máu hồi ở 20%


    // Quản lý tấn công
    public float attackCooldown = 0f; // Thời gian chờ giữa các đòn tấn công
    private float nextAttackTime; // Thời điểm có thể tấn công tiếp theo
    private GameObject bossLazer;

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

    #region Private Value

    private bool isMove = true;
    private string isAttackName;
    private SpriteRenderer spriteRenderer; // Tham chiếu đến SpriteRenderer
    private bool IsFacingRight = true;
    private float currentHealth; // Máu hiện tại
    private int numbarAttack;
    private bool isShield = false;
    private bool isShoot = false;
    private GameObject ArmLazer;
    #endregion

    void Start()
    {
        currentHealth = maxHealth; // Khởi tạo máu
       
        player = GameObject.FindGameObjectWithTag("Player").transform; // Tìm người chơi qua tag

        ChooseNextAttack(); // Chọn đòn tấn công đầu tiên
        bossLazer = GameObject.Find("Arm");
        ArmLazer = GameObject.Find("ArmLazer");
        bossLazer.gameObject.SetActive(false);
        ArmLazer.gameObject.SetActive(false);
        spriteRenderer = GetComponent<SpriteRenderer>();
        AttackPoint = AttackPosition.transform.position;

    }

    void Update()
    {
        if (isMove)
        {
            AttackPoint = AttackPosition.transform.position;

            // Cập nhật kích thước tấn công dựa vào sprite
            if (spriteRenderer != null && spriteRenderer.sprite != null && nextAttack != 1)
            {
                squareAttackSize = spriteRenderer.sprite.bounds.size;
            }
            else
            {
                squareAttackSize = new Vector2(6f, 2f);
            }

            // Tính khoảng cách từ boss đến người chơi
            float distanceToPlayer = Mathf.Abs(AttackPoint.x - player.position.x);
            float currentAttackRange = squareAttackSize.x;

            // Di chuyển nếu ngoài tầm đánh
            if (distanceToPlayer > currentAttackRange)
            {
                MoveTowardsPlayer();
            }
            // Tấn công nếu trong tầm và hết thời gian chờ
            else if (Time.time >= nextAttackTime && !isShield && !isShoot)
            {
                AttackAnimation();
             
                nextAttackTime = Time.time + attackCooldown;
            }

            // Quay mặt boss về phía người chơi
            CheckDirectionToFace(player.position.x > transform.position.x);

            // Kiểm tra hồi máu
            CheckHealing();

            // **Tự động kích hoạt bất tử mỗi 15 giây**
            // **Tự động kích hoạt bất tử mỗi 15 giây**
            if (!isInvulnerable && Time.time >= lastInvulnerableTime + invulnerableCooldown)
            {
                lastInvulnerableTime = Time.time; // Cập nhật lại thời gian kích hoạt trước khi kích hoạt bất tử
                isShield = true;
                ActivateInvulnerability();
            }


        }

        slider.value = currentHealth;
    }

    public void _Shoot()
    {
        isShoot = true;
        bossLazer.gameObject.SetActive(true);
        
    }    
    public void Lazer()
    {
        animator.SetBool("glow", false);
        animator.SetBool("shoot", true);
        Debug.Log(animator.GetBool("glow"));
        Debug.Log(animator.GetBool("shoot"));
    }    

    public void ArmLazerOpen()
    {
        ArmLazer.gameObject.SetActive(true);
    }

    public void stopShoot()
    {
        isShoot = false;
        animator.SetBool("shoot", false);
        ArmLazer.gameObject.SetActive(false);
        bossLazer.gameObject.SetActive(false);
    }    
    public void ActivateInvulnerability()
    {
        if (!isInvulnerable && !string.IsNullOrEmpty(immuneParamName))
        {
            isInvulnerable = true;
            animator.SetBool(immuneParamName, true); // Bật animation miễn nhiễm
            StartCoroutine(DisableInvulnerabilityAfterTime(invulnerableDuration));
        }
    }


    private IEnumerator DisableInvulnerabilityAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);

        if (!isInvulnerable) yield break;
        isShield = false;
        isInvulnerable = false;
        animator.SetBool(immuneParamName, false); // Tắt animation miễn nhiễm
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

    public void ChooseNextAttack()
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
        if (isInvulnerable) return;

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