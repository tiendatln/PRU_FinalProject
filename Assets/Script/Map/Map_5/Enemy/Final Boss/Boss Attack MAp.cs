using UnityEngine;
using UnityEngine.Tilemaps;

public class BossAttackMAp : MonoBehaviour
{
    public Vector2 squareAttackSize;
    public GameObject _attackPoint;
    private Transform player;
    private Vector2 AttackPoint;
    public LayerMask layerMask;
    private TilemapRenderer tilemapRenderer;
    void Start()
    {
       
        player = GameObject.FindGameObjectWithTag("Player").transform; // Tìm người chơi qua tag
        tilemapRenderer = GetComponent<TilemapRenderer>();

        //squareAttackSize = tilemapRenderer.
        AttackPoint = tilemapRenderer.transform.position;
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(AttackPoint, squareAttackSize, layerMask);
        foreach (Collider2D enemy in hitEnemies)
        {

            // Kiểm tra xem enemy có component EnemyAI_2D hay không
            if (enemy.gameObject.TryGetComponent<DamageReceived>(out DamageReceived player))
            {
                player.TakeDamage(30); // Gây sát thương lên kẻ địch
            }

        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(AttackPoint, new Vector3(squareAttackSize.x, squareAttackSize.y, 0));
    }
}
