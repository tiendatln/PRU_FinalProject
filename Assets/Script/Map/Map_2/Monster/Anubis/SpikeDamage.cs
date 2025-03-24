using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    public Vector2 damageArea = new Vector2(1f, 1f); // Khu vực gây sát thương
    public float damage = 10f; // Sát thương gây ra
    public LayerMask playerMask; // Chỉ gây sát thương cho Player

    public void SendDamage()
    {
        Collider2D[] hitPlayers = Physics2D.OverlapBoxAll(transform.position, damageArea, 0, playerMask);
        foreach (Collider2D player in hitPlayers)
        {
            if (player.TryGetComponent<DamageReceived>(out DamageReceived damageReceiver))
            {
                damageReceiver.TakeDamage(damage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(damageArea.x, damageArea.y, 0));
    }
}
