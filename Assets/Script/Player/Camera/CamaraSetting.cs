using Unity.Cinemachine;
using UnityEngine;

public class CamaraSetting : MonoBehaviour
{
    public GameObject backgournd;
    public GameObject reSpawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        backgournd = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x * 0.1f, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EditorOnly"))
        {
            transform.position = new Vector2(reSpawn.transform.position.x, reSpawn.transform.position.y);
            //Vector2 p = new Vector2(reSpawn.transform.position.x, reSpawn.transform.position.y);
            //Instantiate(backgournd, new Vector2(reSpawn.transform.position.x, reSpawn.transform.position.y), transform.rotation);
        }
    }

   
}
