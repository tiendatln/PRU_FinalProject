using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    private GameObject audioSetting;
    private GameObject pauseMemu;
    private Slider SFXVolumeSlider;
    private Slider MusicVolumeSlider;
    [Header("Image Volume")]
    public Sprite _muteSFXVolume;
    public Sprite _noMuteSFXVolume;
    public Sprite _muteMusicVolume;
    public Sprite _noMuteMusicVolume;

    private Image SFXImg;
    private Image MusicImg;

    private void Start()
    {
        Invoke("FindOnject", 0.0000001f);
    }

    public void FindOnject()
    {
        audioSetting = GameObject.Find("Audio Setting");
        SFXVolumeSlider = GameObject.Find("SfxSlider").GetComponent<Slider>();
        MusicVolumeSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();

        SFXImg = GameObject.Find("SFXImage").GetComponent<Image>();
        MusicImg = GameObject.Find("MusicImage").GetComponent<Image>();
        audioSetting.SetActive(false);
        pauseMemu = GameObject.Find("Pause Menu");
    }

    public void ContinueBtn()
    {
        Time.timeScale = 1.0f;
        MouseOff();
        pauseMemu.SetActive(false);
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

    public void AudioSettingBtn()
    {
        
        if (audioSetting.activeInHierarchy == true)
        {
            audioSetting.SetActive(false);
        }
        else
        {
            audioSetting.SetActive(true);
            SFXVolumeSlider.value = GameManager.Instance.getMainData().SFXVolume;
            MusicVolumeSlider.value = GameManager.Instance.getMainData().musicVolume;
        }
    }

    public void settingSFXVolume()
    {
        AudioManager.Instance.setSFXVolume(SFXVolumeSlider.value);

        
        if (SFXVolumeSlider.value == 0)
        {
            SFXImg.sprite = _muteSFXVolume;
        }
        else
        {
            SFXImg.sprite = _noMuteSFXVolume;
        }
    }

    public void settingMusicVolume()
    {
        AudioManager.Instance.setMusicVolume(MusicVolumeSlider.value);
        if (MusicVolumeSlider.value == 0)
        {
            MusicImg.sprite = _muteMusicVolume;
        }
        else
        {
            MusicImg.sprite = _noMuteMusicVolume;
        }
    }

    public void muteSFXVolume()
    {
        if (SFXVolumeSlider.value > 0)
        {
            AudioManager.Instance.setSFXVolume(0);
            SFXVolumeSlider.value = 0;
            SFXImg.sprite = _muteSFXVolume;
            
        }
        else
        {
            AudioManager.Instance.setSFXVolume(0.3f);
            SFXVolumeSlider.value = 0.3f;
            SFXImg.sprite = _noMuteSFXVolume;
            
        }
    }

    public void muteMusicVolume()
    {
        if (MusicVolumeSlider.value > 0)
        {
            AudioManager.Instance.setMusicVolume(0);
            MusicVolumeSlider.value = 0;
            MusicImg.sprite = _muteMusicVolume;
            
        }
        else
        {
            AudioManager.Instance.setMusicVolume(0.3f);
            MusicVolumeSlider.value = 0.3f;
            MusicImg.sprite = _noMuteMusicVolume;
           
        }

    }

    public void Save()
    {
        GameManager.Instance.getMainData().SavePlayer();
    }
}
