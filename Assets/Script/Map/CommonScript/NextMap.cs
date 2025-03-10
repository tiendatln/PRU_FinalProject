using UnityEngine;
using UnityEngine.SceneManagement;

public class NextMap : MonoBehaviour
{
    public void nextMap()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // lấy sceen hiện tại + 1 để chuyển sceen
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                nextMap();
            }
        }
    }
}
