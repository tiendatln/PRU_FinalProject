using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    protected PlMove PlMove;
    protected PlayerAttack PlayerAttack;
    protected DamageReceived DamageReceived;
    protected PlayerAnimation PlayerAnimation;
    [SerializeField] private SpawnMagicSkill SpawnMagicSkill;
    public PlayerMainData PlayerMainData;
    public PlayerLever PlayerLever;

    public float DashCoolDown;
    
    void Awake()
    {
        PlayerAttack = GetComponent<PlayerAttack>();
        PlMove = GetComponent<PlMove>();
        DamageReceived = GetComponent<DamageReceived>();
        PlayerAnimation = GetComponent<PlayerAnimation>();
        PlayerLever = GetComponent<PlayerLever>();
        transform.position = PlayerMainData.GetVectorPLayer();
    }

    void Update()
    {
        #region Time
        DashCoolDown -= Time.deltaTime;
        #endregion
        if (Input.GetKeyDown(KeyCode.I) && SpawnMagicSkill.MagicCoolDownSkill < 0)
        {
            PlayerAnimation.NomalMagicSkill();
            PlMove.StopJump(0.2f);

        }
        if (Input.GetKeyDown(KeyCode.K) && SpawnMagicSkill.ArrowCoolDownSkill < 0)
        {
            PlayerAnimation.BowAttack();
            PlMove.StopJump(0.43f);
        }

        if (PlMove.rb.linearVelocityY < 0 && PlMove.LastOnGroundTime < 0)
        {
            PlayerAnimation.Fallen(true);
        }
        if (PlMove.LastOnGroundTime > 0 && PlayerAnimation.animator.GetBool("fallen"))
        {
            PlayerAnimation.Fallen(false);
        }
        
        
    }


    public void ArrowShoot()
    {
        SpawnMagicSkill.ArrowShoot();
        PlayerAnimation.EndBowAttack();
    }

    public void MagicShoot()
    {
        SpawnMagicSkill.MagicShoot();
        PlayerAnimation.EndMagicSkill();
    }

    public void SetCanMove()
    {
        PlMove.canMove = 0;
        PlMove._moveInput = Vector2.zero;
    }


    
}
