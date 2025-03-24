using UnityEngine;

public class LeverTrap : MonoBehaviour
{
    public GameObject[] Monster;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            for ()
            {

            }
        }
    }
}
