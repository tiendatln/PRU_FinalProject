using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public PlayerMainData playerMainData;

    private void Start()
    {
        
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        
    }
    public void StartNew()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        playerMainData.NewGame(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(playerMainData.Mapindex);
        
    }
    public void OptionBtn()
    {

    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
