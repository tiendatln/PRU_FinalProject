using UnityEngine;

public class Magic : MonoBehaviour
{
    public Animator animator;
    public GameObject skill;
    public GameObject player;
    public PlayerController playerController;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Character");
        playerController = player.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyAI_2D>(out EnemyAI_2D enemy))
        {
            enemy.TakeDamage(playerController.PlayerMainData.attackSkill);
            skill.SetActive(false);
        }
        else if (collision.CompareTag("Ground"))
        {
            skill.SetActive(false);
        }
        else if (collision.gameObject.TryGetComponent<Boss>(out Boss boss))
        {
            boss.TakeDamage(playerController.PlayerMainData.attackSkill);
            skill.SetActive(false);
        }

    }
}
