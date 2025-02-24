using UnityEngine;

public class PlayerController : MonoBehaviour
{

    protected PlMove PlMove;
    protected PlayerAttack PlayerAttack;
    protected DamageReceived DamageReceived;
    protected PlayerAnimation PlayerAnimation;
    [SerializeField] private SpawnMagicSkill SpawnMagicSkill;

    void Awake()
    {
        PlayerAttack = GetComponent<PlayerAttack>();
        PlMove = GetComponent<PlMove>();
        DamageReceived = GetComponent<DamageReceived>();
        PlayerAnimation = GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            PlayerAnimation.NomalMagicSkill();
            SpawnMagicSkill.Shoot(PlMove.CheckInput());
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("enemy"))
        {
            DamageReceived.TakeDamage(PlMove.CheckInput());
        }
    }

    
}
