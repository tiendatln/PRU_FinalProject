using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class PlayerController : MonoBehaviour
{

    public PlMove PlMove;
    public PlayerAttack PlayerAttack;
    public DamageReceived DamageReceived;
    public PlayerAnimation PlayerAnimation;
    [SerializeField] private SpawnMagicSkill SpawnMagicSkill;
    public PlayerLever PlayerLever;
    public SpawnMagicSkill PlayerSpawnMagicSkill;
    public float DashCoolDown;

    public PlayerMainData PlayerMainData; // Chứa data của Player xuyên suốt các màn chơi và khi load hoặc tắt game (SaveGame)

    void Awake()
    {
        PlayerAttack = GetComponent<PlayerAttack>();
        PlMove = GetComponent<PlMove>();
        DamageReceived = GetComponent<DamageReceived>();
        PlayerAnimation = GetComponent<PlayerAnimation>();
        PlayerLever = GetComponent<PlayerLever>();
        PlayerSpawnMagicSkill = GameObject.Find("SpawnSkill").gameObject.GetComponent<SpawnMagicSkill>();
        transform.position = new Vector3(PlayerMainData.PlayerPosition[0], PlayerMainData.PlayerPosition[1], PlayerMainData.PlayerPosition[2]);
        Time.timeScale = 1f;
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
