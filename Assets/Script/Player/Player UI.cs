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
            PauseMenu.SetActive(true);
        }else if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale.Equals(0f))
        {
            Time.timeScale = 1f;
            PauseMenu.SetActive(false);
        }
    }

    public virtual void SetHeath(float heath)
    {
        Slider.value = heath;
    }
}
