
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class MonsterSpawnSKill : MonoBehaviour
{
    public GameObject skil;
    private EnemyAI_2D _enemyAI;
    public float SkillSpeed;
    private Transform player; // Gán Player vào Inspector
    public float rotationSpeed = 5f;
    public AssetLabelReference fireBall;
    private AsyncOperationHandle<GameObject> Handle;

    private void Start()
    {
        _enemyAI = GameObject.FindGameObjectWithTag("enemy").GetComponent<EnemyAI_2D>();
        player = GameObject.Find("Character").transform;

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Update()
    {

        if (player != null)
        {
            // Tính hướng quay
            Vector3 direction = (player.position - transform.position).normalized;

            // Chuyển hướng thành góc quay cho 2D
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Áp dụng góc quay theo trục Z thay vì sử dụng LookRotation
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

            // Quay dần về hướng Player 
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }


    }


    public virtual void Shoot()
    {
        if (fireBall == null || string.IsNullOrEmpty(fireBall.labelString))
        {

            return;
        }

        Handle = Addressables.LoadAssetAsync<GameObject>(fireBall.labelString);
        Handle.Completed += (AsyncOperationHandle<GameObject> task) =>
        {
            GameObject fire = MyPoolManager.instance.GetFromPool(task.Result);
            fire.transform.position = transform.position;
            fire.transform.rotation = transform.rotation;
            Rigidbody2D rb = fire.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(transform.right * SkillSpeed, ForceMode2D.Impulse);
            }
        };

    }
    void UnloadAsset()
    {
        if (Handle.IsValid())
        {
            Addressables.Release(Handle); // Releases the asset from memory
        }
    }


    void OnDestroy()
    {
        UnloadAsset(); // Clean up when the object is destroyed
    }
}
