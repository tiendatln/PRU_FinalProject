using System.Collections;
using UnityEngine;

public class LeverTrap : MonoBehaviour
{
    public GameObject[] Monster;
    int count = 0;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.F) && count == 0)
        {
            StartCoroutine(SpawnMonster());
            count++;
        }
    }

    private IEnumerator SpawnMonster()
    {
        for (int i = 0; i < Monster.Length; i++)
        {
            Instantiate(Monster[i], this.transform.position - new Vector3(2 + i,-1,0), Monster[i].transform.rotation);
            yield return null;
        }
    }
}
