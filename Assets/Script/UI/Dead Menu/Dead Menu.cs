using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadMenu : MonoBehaviour
{
    public string scene;
    public PlayerMainData playerMainData;
    public void Reset()
    {
        if (Application.isPlaying)
        {
            SceneManager.LoadScene(scene);
            Time.timeScale = 1f;
            playerMainData.health = 100;
            playerMainData.CheckPointNew();
        }
    }
}
