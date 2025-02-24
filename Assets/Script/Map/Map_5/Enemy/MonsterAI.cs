using System.Collections;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public float speed = 2f;
    public float patrolDistance = 3f;
    public float detectionRange = 5f;
    public float attackRange = 1f;
    public int attackDamage = 10;
    public float attackCooldown = 1.5f;

    private Vector2 startPos;
    private bool movingRight = false;
    private Transform player;
    private float lastAttackTime;
    public Rigidbody2D rb;
    public Animator animator;
    private bool isMove = true;
    private bool isAttack = true;
    private bool IsFacingRight = true;


    void Start()
    {
        startPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isMove)
        {
            if (player != null && Vector2.Distance(transform.position, player.position) <= detectionRange)
            {
                ChasePlayer();
            }
            else
            {
                
                    animator.SetFloat("Walk", 2f);
                    Patrol();
            }
        }

    }

    void Patrol()
    {
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (transform.position.x >= startPos.x + patrolDistance)
            {
                movingRight = false;
                
                    CheckDirectionToFace(patrolDistance > 0);
                
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (transform.position.x <= startPos.x - patrolDistance)
            {
                movingRight = true;
                CheckDirectionToFace(-patrolDistance > 0);
                
            }
        }
    }

    void ChasePlayer()
    {
        float direction = (player.position.x > transform.position.x) ? 1 : -1;
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
        CheckDirectionToFace(direction > 0);

        if (Vector2.Distance(transform.position, player.position) <= attackRange && Time.time > lastAttackTime + attackCooldown && isAttack)
        {
            Attack();
        }
    }

    void Attack()
    {
        Debug.Log("Quái vật tấn công nhân vật!");
        lastAttackTime = Time.time;
        // Thêm code gây sát thương cho nhân vật ở đây
        animator.SetBool("Cleave", true);
        Invoke("StopAttack", 1.5f);
        StartCoroutine(DisableControlTemporarily(1.5f));
    }
    IEnumerator DisableControlTemporarily(float time)
    {
        isMove = false;
        yield return new WaitForSeconds(time); // Chờ một khoảng thời gian
        isMove = true;
    }
    void StopAttack()
    {
        animator.SetBool("Cleave", false);
    }



    #region Turn
    private void Turn()
    {
        //stores scale and flips the player along the x axis, 
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.y -= -180;
        transform.rotation = Quaternion.Euler(rotation);

        IsFacingRight = !IsFacingRight;
    }
    #endregion

    #region CHECK METHODS
    public void CheckDirectionToFace(bool isMovingRight)
    {
        if (isMovingRight != IsFacingRight)
            Turn();
    }
    #endregion

    void TakeDamage()
    {

    }
}
