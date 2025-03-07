using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject MainMenu;
    public void ContinueBtn()
    {
        Time.timeScale = 1.0f;
        MainMenu.SetActive(false);
    }
    

    public void BackToMenuBtn()
    {
        SceneManager.LoadScene("Main Menu Game");
    }
}
