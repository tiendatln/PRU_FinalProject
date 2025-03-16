using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BossLazer : MonoBehaviour
{
    public float _damage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController PlayerController))
        {
            PlayerController.DamageReceived.TakeDamage(_damage);
            
        }
    }
}
