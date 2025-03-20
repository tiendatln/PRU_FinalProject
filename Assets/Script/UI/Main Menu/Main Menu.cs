using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public PlayerMainData playerMainData;
    private GameObject tutorial;
    private GameObject audioSetting;

    private void Start()
    {
            
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        tutorial = GameObject.Find("Tutorial Menu");
        tutorial.SetActive(false);

        audioSetting = GameObject.Find("Audio Setting");
        audioSetting.SetActive(false);
    }
    public void StartNew()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        playerMainData.NewGame(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(playerMainData.indexOfCurrentMap);
        
    }
    public void AudioBtn()
    {
        if (audioSetting.active == true)
        {
            audioSetting.SetActive(false);
        }
        else
        {
            audioSetting.SetActive(true);
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void TutorialBtn()
    {
        if (tutorial.active == true)
        {
            tutorial.SetActive(false);
        }
        else
        {
            tutorial.SetActive(true);
        }
    }
}
