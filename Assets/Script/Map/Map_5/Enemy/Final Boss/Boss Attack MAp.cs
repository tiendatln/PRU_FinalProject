using UnityEngine;
using UnityEngine.Tilemaps;

public class BossAttackMAp : MonoBehaviour
{
    public float _damage;



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController player))
        {
            player.DamageReceived.TakeDamage(_damage); // Gây sát thương lên kẻ địch
        }
    }
}
