using UnityEngine;
using UnityEngine.SceneManagement;

public class NextMap : MonoBehaviour
{
    public void nextMap()
    {
        GameManager.Instance.getMainData().indexOfCurrentMap = SceneManager.GetActiveScene().buildIndex + 1;

        GameManager.Instance.getMainData().setPositionNextMap(); // set vị trí của nhân vật là vị trí cổng ở map tiếp theo

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
