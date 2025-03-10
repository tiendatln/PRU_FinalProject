using System.Collections.Generic;
using UnityEngine;

public class MyPoolManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static MyPoolManager instance;
    private Dictionary<GameObject, MyObjectPool> dicPool = new Dictionary<GameObject, MyObjectPool>();

    private void Awake()
    {
        instance = this;
    }

    public GameObject GetFromPool(GameObject obj)
    {
        if (dicPool.ContainsKey(obj) == false)
        {
            dicPool.Add(obj, new MyObjectPool(obj));
        }
        return dicPool[obj].get();
    }
}
