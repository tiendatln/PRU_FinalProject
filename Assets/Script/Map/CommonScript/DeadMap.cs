using UnityEngine;

public class DeadMap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.GetPlayerData().health = 0;
    }
}
