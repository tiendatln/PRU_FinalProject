using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

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

            // Tạo góc quay mới nhưng giữ nguyên trục Y
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));

            // Xoay 90 độ quanh trục Y để trục X hướng về player
            lookRotation *= Quaternion.Euler(0, -90, 0);

            // Quay dần về hướng Player 
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

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
             GameObject fire = MyPoolManager.instance.GetFromPool(skil);
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
