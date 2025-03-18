using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeathForPlayer : MonoBehaviour
{
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            
                int ran = Random.Range(-5, 5);
                rb.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
                rb.AddForce(Vector2.right * ran, ForceMode2D.Impulse);
            
        }
        StartCoroutine(ActiveHeath());
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.GetPlayerData().Heal(5);
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator ActiveHeath()
    {
        yield return new WaitForSeconds(5);
        this.gameObject.SetActive(false);
    }
}
