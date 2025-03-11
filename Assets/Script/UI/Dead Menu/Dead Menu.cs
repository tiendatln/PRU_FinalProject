using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadMenu : MonoBehaviour
{
    public string scene;
    public PlayerMainData playerMainData;
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
            SceneManager.LoadScene(scene);
            Time.timeScale = 1f;
            playerMainData.health = 100;
            playerMainData.CheckPointNew(SceneManager.GetActiveScene().buildIndex);
        }

    }
}
