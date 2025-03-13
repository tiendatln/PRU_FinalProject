using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject MainMenu;
    public void ContinueBtn()
    {
        Time.timeScale = 1.0f;
        MouseOff();


        MainMenu.SetActive(false);
    }
    public void MouseOff()
    {
        Cursor.visible = false;

        // (Tùy chọn) Khóa con trỏ ở giữa màn hình
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void BackToMenuBtn()
    {
        GameManager.Instance.GetPlayerData().SavePlayer();
        SceneManager.LoadScene("Main Menu Game");
    }

    public void Save()
    {
        GameManager.Instance.GetPlayerData().SavePlayer();
    }
}
