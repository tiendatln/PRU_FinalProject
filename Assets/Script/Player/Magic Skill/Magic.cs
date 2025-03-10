using UnityEngine;

public class Magic : MonoBehaviour
{
    public Animator animator;
    public GameObject skill;
    private GameObject player;
    private PlayerController playerController;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        skill = this.gameObject;
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
        if (collision.CompareTag("Ground"))
        {
            if (skill == null)
            {
                Debug.LogError("Skill GameObject chưa được gán!");
            }
            else
            {
                skill.SetActive(false);
                Debug.Log("Skill đã được tắt khi chạm Ground");
            }
        }
        if (collision.gameObject.TryGetComponent<Boss>(out Boss boss))
        {
            boss.TakeDamage(playerController.PlayerMainData.attackSkill);
            skill.SetActive(false);
        }
        if (collision.gameObject.TryGetComponent<Medusa>(out Medusa medusa))
        {
            medusa.TakeDamage(playerController.PlayerMainData.attack);
        }
    }
}
