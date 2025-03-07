using UnityEngine;

public class Skill : MonoBehaviour
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
        if (collision.gameObject.TryGetComponent<DamageReceived>(out DamageReceived player))
        {
            player.TakeDamage(_Damage);

            Invoke("StopAnimation", 0.35f);
            Destroy(this);

        }
        else if (collision.CompareTag("Ground"))
        {
            Destroy(this);
        }

    }

    void StopAnimation()
    {
        animator.SetBool("Explosion", true);
        skill.SetActive(false);
    }

}
