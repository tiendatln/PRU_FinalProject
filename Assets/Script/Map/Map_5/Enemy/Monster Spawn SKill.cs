using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class MonsterSpawnSKill : MonoBehaviour
{
    public GameObject skil;
    private EnemyAI_2D _enemyAI;
    public float SkillSpeed;
    public Transform player; // Gán Player vào Inspector
    public float rotationSpeed = 5f;
    public AssetLabelReference fireBall;


    private void Start()
    {
        _enemyAI = GameObject.FindGameObjectWithTag("enemy").GetComponent<EnemyAI_2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Update()
    {
        
        if (player != null && _enemyAI.distanceToPlayer <= _enemyAI.detectionRange)
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

        var handle = Addressables.LoadAssetAsync<GameObject>(fireBall.labelString);
        handle.Completed += (AsyncOperationHandle<GameObject> task) =>
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
}
