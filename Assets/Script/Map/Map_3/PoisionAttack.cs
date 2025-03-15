using UnityEngine;

public class PoisionAttack : MonoBehaviour
{
    [SerializeField] private float damage = 0.0001f;
    [SerializeField] private float lifetime = 5f; // Destroy projectile after this time

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DamageReceived player = other.GetComponent<DamageReceived>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
            Destroy(gameObject); // Destroy projectile on impact
        }
    }
}
