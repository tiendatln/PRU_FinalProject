using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public PlayerMainData playerMainData;
    public string SceneName;
    private void Start()
    {
        
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        
    }
    public void StartNew()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        playerMainData.NewGame();
        
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        
    }
    public void OptionBtn()
    {

    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
