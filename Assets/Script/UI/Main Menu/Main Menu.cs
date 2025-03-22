using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public PlayerMainData playerMainData;
    private GameObject tutorial;
    private GameObject audioSetting;

    public AudioClip mainMusic;
    public AudioClip buttonClick;

    
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
        
        AudioManager.Instance.playMusicLoopSound(mainMusic);
        Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        tutorial = GameObject.Find("Tutorial Menu");
        tutorial.SetActive(false);

        SFXVolumeSlider = GameObject.Find("SfxSlider").GetComponent<Slider>();
        MusicVolumeSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();
        
        SFXImg = GameObject.Find("SFXImage").GetComponent<Image>();
        MusicImg = GameObject.Find("MusicImage").GetComponent<Image>();

        audioSetting = GameObject.Find("Audio Setting");
        audioSetting.SetActive(false);

        if (GameManager.Instance.GetPlayerData().isNewGame == true)
        {
            GameObject.Find("Load Btn").SetActive(false);
        }
        else
        {
            GameObject.Find("Load Btn").SetActive(true);
        }
    }


    public void StartNew()
    {
        AudioManager.Instance.playSFXSound(buttonClick);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        playerMainData.NewGame(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(playerMainData.indexOfCurrentMap);
        
    }
    public void AudioBtn()
    {
        AudioManager.Instance.playSFXSound(buttonClick);
        if (audioSetting.active == true)
        {
            audioSetting.SetActive(false);
        }
        else
        {
            audioSetting.SetActive(true);
            SFXVolumeSlider.value = AudioManager.Instance.audioSFX[0].volume;
            MusicVolumeSlider.value = AudioManager.Instance.audioMusic.volume;
        }
    }
    public void QuitGame()
    {
        AudioManager.Instance.playSFXSound(buttonClick);
        Application.Quit();
    }

    public void TutorialBtn()
    {
        AudioManager.Instance.playSFXSound(buttonClick);
        if (tutorial.active == true)
        {
            tutorial.SetActive(false);
        }
        else
        {
            tutorial.SetActive(true);
        }
    }


    public void settingSFXVolume()
    {
        AudioManager.Instance.setSFXVolume(SFXVolumeSlider.value);
        AudioManager.Instance.playSFXSound(buttonClick);
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
            MusicImg.sprite= _noMuteMusicVolume;
        }
    }

    public void muteSFXVolume()
    {
        if (SFXVolumeSlider.value > 0)
        {
            AudioManager.Instance.setSFXVolume(0);
            SFXVolumeSlider.value = 0;
            SFXImg.sprite = _muteSFXVolume;
            //SFXImg.color = Color.HSVToRGB(230,125,171);
        }
        else
        {
            AudioManager.Instance.setSFXVolume(0.1f);
            SFXVolumeSlider.value = 0.1f;
            SFXImg.sprite = _noMuteSFXVolume;
            //SFXImg.color = Color.HSVToRGB(230, 125, 171);
        }
    }

    public void muteMusicVolume()
    {
        if (MusicVolumeSlider.value > 0)
        {
            AudioManager.Instance.setMusicVolume(0);
            MusicVolumeSlider.value = 0;
            MusicImg.sprite = _muteMusicVolume;
            //MusicImg.color = Color.HSVToRGB(230, 125, 171);
        }
        else
        {
            AudioManager.Instance.setMusicVolume(0.1f);
            MusicVolumeSlider.value = 0.1f;
            MusicImg.sprite = _noMuteMusicVolume;
            //MusicImg.color = Color.HSVToRGB(230, 125, 171);
        }
        
    }

}
