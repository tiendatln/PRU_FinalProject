using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public void ContinueBtn()
    {
        Time.timeScale = 1.0f;
        MouseOff();
        this.gameObject.SetActive(false);
    }
    public void MouseOff()
    {
        Cursor.visible = false;

        // (Tùy chọn) Khóa con trỏ ở giữa màn hình
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void BackToMenuBtn()
    {
        GameManager.Instance.getMainData().SavePlayer();
        SceneManager.LoadScene("Main Menu Game");
        AudioManager.Instance.stopMusicLoopSound();
    }

    public void Save()
    {
        GameManager.Instance.getMainData().SavePlayer();
    }
}
