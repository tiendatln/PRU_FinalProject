using UnityEngine;

public class PlayerController : MonoBehaviour
{

    protected PlMove PlMove;
    protected PlayerAttack PlayerAttack;
    protected DamageReceived DamageReceived;

    void Awake()
    {
        PlayerAttack = GetComponent<PlayerAttack>();
        PlMove = GetComponent<PlMove>();
        DamageReceived = GetComponent<DamageReceived>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("enemy"))
        {
            DamageReceived.TakeDamage(PlMove.CheckInput());
        }
    }

}
