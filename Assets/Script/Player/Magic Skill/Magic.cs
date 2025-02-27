using UnityEngine;

public class Magic : MonoBehaviour
{
    public Animator animator;
    public GameObject skill;
    public float _Damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyAI_2D>(out EnemyAI_2D enemy))
        {
            enemy.TakeDamage(_Damage);
            skill.SetActive(false);
        }
        else if (collision.CompareTag("Ground"))
        {
            skill.SetActive(false);
        }

    }
}
