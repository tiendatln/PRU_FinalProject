using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Slider Slider;
    public PlayerController controller;
    public GameObject PauseMenu;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale.Equals(1f))
        {
            Time.timeScale = 0f;
            MouseOn();
            PauseMenu.SetActive(true);
        }else if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale.Equals(0f))
        {
            Time.timeScale = 1f;
            PauseMenu.SetActive(false);
        }
    }
    void MouseOn()
    {
        // Bật lại con trỏ khi thoát (tùy chọn)
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void MouseOff()
    {
        Cursor.visible = false;

        // (Tùy chọn) Khóa con trỏ ở giữa màn hình
        Cursor.lockState = CursorLockMode.Locked;
    }

    public virtual void SetHeath(float heath)
    {
        Slider.value = heath;
    }
}
