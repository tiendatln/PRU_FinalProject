using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string SceneName;

    public void StartNew()
    {

    }
    public void LoadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        Time.timeScale = 1.0f;
    }
    public void OptionBtn()
    {

    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
