using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlMove : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("run")]
    public float runMaxSpeed;
    private bool isRunning = true;
    public float LastOnGroundTime { get; private set; }
    public bool IsFacingRight { get; private set; }
    public float runAccelAmount; //The actual force (multiplied with speedDiff) applied to the player.
    public float runDeccelAmount; //Actual force (multiplied with speedDiff) applied to the player .
    [Range(0.01f, 1f)] public float accelInAir;
    [Range(0.01f, 1f)] public float deccelInAir;

    [Header("Jump")]
    public float DashPower;
    public float jumpHeight = 20f;
    private bool isjumping;            
    public bool IsWallJumping { get; private set; }
    public float maxFallSpeed;
    public static float Gravity = 2;
    [Range(0.01f, 0.5f)] public float jumpInputBufferTime;
    [Range(0f, 1)] public float jumpHangGravityMult;
    public float LastPressedJumpTime { get; private set; }

    [Header("Wall Jump")]
    [Range(0f, 1)]public float wallJumpTime;
    private float _wallJumpStartTime;
    private int _lastWallJumpDir;

    [HideInInspector]public Vector2 _moveInput;
    [Header("Checks")]
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private Vector2 _groundCheckSize = new Vector2(0.1f, 0.01f);
    [Header("Layers & Tags")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Vector2 _wallCheckSize = new Vector2(0.02f, 0.25f);
    [SerializeField] private Transform _frontWallCheckPoint;
    [SerializeField] private Transform _backWallCheckPoint;
    public float LastOnWallTime { get; private set; }

    [HideInInspector]public int canMove = 1;
    private bool _isJumpCut;
    private bool _isJumpFalling;
    [Header("Slide")]
    public float slideSpeed;
    public float slideAccel;
    public bool IsSliding { get; private set; }
    public float LastOnWallRightTime { get; private set; }
    public float LastOnWallLeftTime { get; private set; }
    void Awake()
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
        LastOnWallTime -= Time.deltaTime;
        LastOnWallRightTime -= Time.deltaTime;
        LastOnWallLeftTime -= Time.deltaTime;
        LastPressedJumpTime -= Time.deltaTime;
        #endregion

        #region Move Input
        if (canMove != 0)
        {
            _moveInput.x = Input.GetAxisRaw("Horizontal");
            _moveInput.y = Input.GetAxisRaw("Vertical");
        }
        #endregion

        if (_moveInput.x != 0)
            CheckDirectionToFace(_moveInput.x > 0);

        
        Animation();            

        
        #region JUMP
        if(Input.GetKeyDown(KeyCode.Space))
        {
            OnJumpInput();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            OnJumpUpInput();
        }
        
        #region SLIDE CHECKS
        if (CanSlide() && ((LastOnWallRightTime > 0 && _moveInput.x < 0) || (LastOnWallRightTime > 0 && _moveInput.x > 0)))
            IsSliding = true;
        else
            IsSliding = false;
        #endregion
        if (IsSliding)
        {
            
            rb.gravityScale = 0;
        }

        // Kiểm tra nhấn nút nhảy và còn lượt nhảy
        if (CanJump() && LastPressedJumpTime > 0)
        {
            isjumping = true;
            _isJumpCut = false;
            _isJumpFalling = false;
            jump();
        }
        else if (CanWallJump() && LastPressedJumpTime > 0)
        {
            IsWallJumping = true;
            isjumping = false;
            _isJumpCut = false;
            _isJumpFalling = false;

            _wallJumpStartTime = Time.time;
            _lastWallJumpDir = rb.transform.rotation.y >= 0 ? -1 : 1;
            WallJump(_lastWallJumpDir);
        }
        #endregion



        #region JUMP CHECK
        if (isjumping && rb.linearVelocityY < 0)
        {
            isjumping = false;

            _isJumpFalling = true;
        }

        if (IsWallJumping && Time.time - _wallJumpStartTime > wallJumpTime)
        {
            IsWallJumping = false;
        }

        if (LastOnGroundTime > 0 && !isjumping && !IsWallJumping)
        {
            _isJumpCut = false;

            _isJumpFalling = false;
        }

        if (LastOnGroundTime < 0)
        {
            if (rb.linearVelocityY < 0 && !IsSliding)
            {
                rb.gravityScale = (Gravity * 2f);
                rb.linearVelocity = new Vector2(rb.linearVelocityX, Mathf.Max(rb.linearVelocityY, -maxFallSpeed));
            }
            else if (_isJumpCut)
            {
                rb.gravityScale = (Gravity * 3.5f);
                rb.linearVelocity = new Vector2(rb.linearVelocityX, Mathf.Max(rb.linearVelocityY, -maxFallSpeed));
            }
            else if ((isjumping || IsWallJumping || _isJumpFalling) && Mathf.Abs(rb.linearVelocityY) < 0)
            {
                rb.gravityScale = Gravity;
            }
            else
            {
                rb.gravityScale = Gravity;
            }
            
        }
        else
        {
            if(rb.gravityScale != 2)
            {
                rb.gravityScale = Gravity;
                
            }
            
        }
        
        #endregion

        if (!isjumping)
        {

            //Ground Check
            if (Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, _groundLayer) && !isjumping) //checks if set box overlaps with ground
            {
                LastOnGroundTime = 0.1f; //if so sets the lastGrounded to coyoteTime
            }
            if (LastOnWallTime > 0 && LastOnGroundTime < 0)
            {
                animator.SetBool("Wall Idle", true);
            }
            else
            {
                animator.SetBool("Wall Idle", false);
            }
            
            //Right Wall Check
            if (((Physics2D.OverlapBox(_frontWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && IsFacingRight)
					|| (Physics2D.OverlapBox(_backWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && !IsFacingRight)) && !IsWallJumping)
				LastOnWallRightTime = 0.1f;

			//Right Wall Check
			else if (((Physics2D.OverlapBox(_frontWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && !IsFacingRight)
				|| (Physics2D.OverlapBox(_backWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && IsFacingRight)) && !IsWallJumping)
				LastOnWallLeftTime = 0.1f;


            LastOnWallTime = Mathf.Max(LastOnWallLeftTime, LastOnWallRightTime);
            //Debug.Log(LastOnWallTime);
        }

    }
    private void FixedUpdate()
    {
        if (!CanWallJump() && isRunning)
        {
            run();
        }

        if (IsSliding && LastOnGroundTime > 0 && rb.linearVelocityY > 0)
            Slide();
        
    }

    #region Animation
    void Animation()
    {
        if (rb.linearVelocityY >= 0 && LastOnGroundTime > 0)
        {
            animator.SetBool("fallen", false);
        }
    }
    #endregion


    void run()
    {
        float targetSpeed = _moveInput.x * runMaxSpeed;
        float accelRate;
        if (LastOnGroundTime > 0)
        {
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? runAccelAmount : runDeccelAmount;
        }
        else
        {
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? runAccelAmount * accelInAir : runDeccelAmount * deccelInAir;
        }

        #region Add Bonus Jump Apex Acceleration
        //Increase are acceleration and maxSpeed when at the apex of their jump, makes the jump feel a bit more bouncy, responsive and natural
        if ((isjumping || IsWallJumping || _isJumpFalling) && Mathf.Abs(rb.linearVelocityY) < 1)
        {
            accelRate *= 0.25f;
            targetSpeed *= 0.25f;
        }
        #endregion
        float speedDif = targetSpeed - rb.linearVelocityX;
        float movement = (speedDif * accelRate);
        rb.AddForce(movement * Vector2.right, ForceMode2D.Force);
        animator.SetFloat("speed", Mathf.Abs(movement));
    }
    void jump()
    {
        float jumpPower = jumpHeight;
        LastPressedJumpTime = 0;
        LastOnGroundTime = 0;

        if (rb.linearVelocityY < 0)
        {
            jumpPower = jumpHeight - rb.linearVelocityY;
        }
        animator.SetBool("jump", true);
        //animator.SetInteger("Next Attack", 0);
        rb.AddForce(jumpPower * Vector2.up, ForceMode2D.Impulse);

    }

    void WallJump(int dir)
    {
        //Ensures we can't call Wall Jump multiple times from one press
        LastPressedJumpTime = 0;
        LastOnGroundTime = 0;
        LastOnWallRightTime = 0;
        LastOnWallLeftTime = 0;

        #region Perform Wall Jump
        Vector2 force = new Vector2(10, 12);
        force.x *= dir; //apply force in opposite direction of wall

        if (Mathf.Sign(rb.linearVelocityX) != Mathf.Sign(force.x))
            force.x -= rb.linearVelocityX;

        if (rb.linearVelocityY < 0) //checks whether player is falling, if so we subtract the velocity.y (counteracting force of gravity). This ensures the player always reaches our desired jump force or greater
            force.y -= rb.linearVelocityY;

        //Unlike in the run we want to use the Impulse mode.
        //The default mode will apply are force instantly ignoring masss
        rb.AddForce(force.y * Vector2.up, ForceMode2D.Impulse);
        rb.AddForce(force.x * Vector2.right, ForceMode2D.Impulse);
        animator.SetBool("jump", true);
        
        #endregion
    }


    #region OTHER MOVEMENT METHODS
    private void Slide()
    {
        //We remove the remaining upwards Impulse to prevent upwards sliding
        if (rb.linearVelocityY > 0)
        {
            rb.AddForce(-rb.linearVelocityY * Vector2.up, ForceMode2D.Impulse);
        }

        //Works the same as the Run but only in the y-axis
        //THis seems to work fine, buit maybe you'll find a better way to implement a slide into this system
        float speedDif = slideSpeed - rb.linearVelocityY;
        float movement = speedDif * slideAccel;
        //So, we clamp the movement here to prevent any over corrections (these aren't noticeable in the Run)
        //The force applied can't be greater than the (negative) speedDifference * by how many times a second FixedUpdate() is called. For more info research how force are applied to rigidbodies.
        movement = Mathf.Clamp(movement, -Mathf.Abs(speedDif) * (1 / Time.fixedDeltaTime), Mathf.Abs(speedDif) * (1 / Time.fixedDeltaTime));

        rb.AddForce(movement * Vector2.up);
    }
    #endregion

    public void OnJumpInput()
    {
        LastPressedJumpTime = jumpInputBufferTime;
    }
    public void OnJumpUpInput()
    {
        if (CanJumpCut() || CanWallJumpCut())
            _isJumpCut = true;
        
    }

    private bool CanJumpCut()
    {
        return isjumping && rb.linearVelocityY > 0;
    }

    private bool CanWallJumpCut()
    {
        return IsWallJumping && rb.linearVelocityY > 0;
    }

    public bool CanWallJump()
    {
        return LastPressedJumpTime > 0 && LastOnWallTime > 0 && LastOnGroundTime <= 0 && (!IsWallJumping ||
             (LastOnWallRightTime > 0 && _lastWallJumpDir == 1) || (LastOnWallLeftTime > 0 && _lastWallJumpDir == -1)); ;
    }

    public bool CanJump()
    {
        return !isjumping && LastOnGroundTime > 0;
    }
    public bool CanSlide()
    {
        if ( LastOnWallTime > 0 && !IsWallJumping && !isjumping && LastOnGroundTime <= 0)
            return true;
        else
            return false;
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

    public virtual float CheckInput()
    {
        Vector3 rota = transform.rotation.eulerAngles;
        return rota.y > 1? 1 : -1;
    }

    public virtual void CanMove(float time)
    {
        StartCoroutine(DisableControlTemporarily(time));
    }

    public virtual void StopJump(float time)
    {
        
        StartCoroutine(DisableControlTemporarily(time));

    }
    IEnumerator DisableControlTemporarily(float time)
    {
        rb.linearVelocity = Vector2.zero;
        animator.SetBool("jump", false);
        isRunning = false;
        yield return new WaitForSeconds(time); // Chờ một khoảng thời gian
        
        isRunning = true;
    }

    
}