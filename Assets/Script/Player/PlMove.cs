using UnityEngine;

public class PlMove : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("run")]
    public float runMaxSpeed = 30f;
    public float LastOnGroundTime { get; private set; }
    public bool IsFacingRight { get; private set; }
    private float runAccelAmount = 8f; //The actual force (multiplied with speedDiff) applied to the player.
    private float runDeccelAmount = 6f; //Actual force (multiplied with speedDiff) applied to the player .
    private float accelInAir = 0.9f;
    private float deccelInAir = 0.1f;

    [Header("Jump")]
    public float jumpHeight = 20f;
    private bool isjumping;
    public int maxJumps = 2;
    private int jumpCount;               // Đếm số lần nhảy đã thực hiện
    private bool isGrounded;
    [Range(0.01f, 0.5f)] public float jumpInputBufferTime;
    public float LastPressedJumpTime { get; private set; }

    private Vector2 _moveInput;
    [Header("Checks")]
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private Vector2 _groundCheckSize = new Vector2(0.49f, 0.03f);
    [Header("Layers & Tags")]
    [SerializeField] private LayerMask _groundLayer;
    private bool canMove = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        IsFacingRight = true;
        animator = GetComponent<Animator>();
        rb.gravityScale = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        #region TIMERS
        LastOnGroundTime -= Time.deltaTime;
        LastPressedJumpTime -= Time.deltaTime;
        #endregion

        #region Move Input
        if (canMove)
        {
            _moveInput.x = Input.GetAxisRaw("Horizontal");
            _moveInput.y = Input.GetAxisRaw("Vertical");
        }
        #endregion

        if (_moveInput.x != 0)
            CheckDirectionToFace(_moveInput.x > 0);

        isGrounded = Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, _groundLayer);
        if (isGrounded)
        {
            isjumping = false;
        }
        Animation();
        if (isGrounded) //checks if set box overlaps with ground
            LastOnGroundTime = 0.1f;

        if (isGrounded)
        {
            jumpCount = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJumpInput();
        }

        // Kiểm tra nhấn nút nhảy và còn lượt nhảy
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps && (CanJump() || CanJumpCut()))
        {
            jumpCount++;
            jump();
        }

    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            run();
        }
        
    }

    #region Animation
    void Animation()
    {
        if (rb.linearVelocityY < -1)
        {
            animator.SetBool("jump", false);
            animator.SetBool("fallen", true);
        }
        else if (rb.linearVelocityY >= 0)
        {
            animator.SetBool("fallen", false);
        }
    }
    #endregion


    void run()
    {
        float targetSpeed = _moveInput.x * runMaxSpeed;

        float speedDif = targetSpeed - rb.linearVelocityX;

        float accelRate;
        if (!isjumping)
        {
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? runAccelAmount : runDeccelAmount;
        }
        else
        {
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? runAccelAmount * accelInAir : runDeccelAmount * deccelInAir;
        }

        if (Mathf.Abs(rb.linearVelocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(rb.linearVelocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && LastOnGroundTime > 0)
        {
            //Prevent any deceleration from happening, or in other words conserve are current momentum
            //You could experiment with allowing for the player to slightly increae their speed whilst in this "state"
            accelRate = 0;
            Debug.Log(accelRate);
        }
        
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, 0.9f) * Mathf.Sign(speedDif);
        rb.AddForce(movement * Vector2.right, ForceMode2D.Force);
        animator.SetFloat("speed",Mathf.Abs(movement));
    }
    void jump()
    {
        rb.gravityScale = 2;
        float jumpPower = 0f;
        jumpPower = jumpHeight;
        LastPressedJumpTime = 0;
        if (rb.linearVelocityY < 0)
        {
            jumpPower = jumpHeight - rb.linearVelocityY;
            rb.gravityScale = 2 * 1.5f;
        }
        animator.SetBool("jump", true);
        animator.SetInteger("Next Attack", 0);
        rb.AddForce(jumpPower * Vector2.up, ForceMode2D.Impulse);
       
        isjumping = true;
        
    }

    public void OnJumpInput()
    {
        LastPressedJumpTime = jumpInputBufferTime;
    }

    private bool CanJumpCut()
    {
        return !isjumping && rb.linearVelocity.y < 0;
    }


    private bool CanJump()
    {
        return !isjumping && LastOnGroundTime > 0;
    }

    #region Turn
    private void Turn()
    {
        //stores scale and flips the player along the x axis, 
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

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

    public virtual float CheckInput()
    {
        return 1f;
    }

    public virtual void CanMove(bool _canMove)
    {
        canMove = _canMove;
        if (!canMove)
        {
            _moveInput.x = 0;
        }
        rb.linearVelocity = Vector3.zero;
    }

    public virtual void StopJump(int gravity)
    {
        if (rb.linearVelocityY > 0)
        {
            rb.gravityScale = gravity;
            if (gravity == 0)
            {
                _moveInput.x = 0;
            }
        }
        animator.SetBool("jump", false);
        rb.linearVelocity = Vector3.zero;
    }
}
