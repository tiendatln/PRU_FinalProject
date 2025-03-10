using UnityEngine;

public class ReturnToMyPool : MonoBehaviour
{
    public MyObjectPool pool;

    public void OnDisable()
    {
        pool.AddToPool(gameObject);
    }
}
