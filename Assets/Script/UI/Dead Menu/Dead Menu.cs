using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadMenu : MonoBehaviour
{
    private void Start()
    {
       
            // Bật lại con trỏ khi thoát (tùy chọn)
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        
    }

    public void Reset()
    {
        if (Application.isPlaying)
        {
            SceneManager.LoadScene(GameManager.Instance.GetPlayerData().indexOfCurrentMap);
            Time.timeScale = 1f;
            GameManager.Instance.GetPlayerData().health = 100;
            GameManager.Instance.GetPlayerData().CheckPointNew(SceneManager.GetActiveScene().buildIndex);
        }

    }
}
