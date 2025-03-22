using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance => _instance;

    public AudioSource[] audioSFX;
    public AudioSource audioMusic;

    private void Awake()
    {
        audioSFX[0].volume = GameManager.Instance.GetPlayerData().SFXVolume;
        audioMusic.volume = GameManager.Instance.GetPlayerData().musicVolume;


        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void playSFXSound(AudioClip _SFXSound)
    {
        for (int i = 0; i < audioSFX.Length; i++)
        {
            if (audioSFX[i].isPlaying == false)
            {
                audioSFX[i].clip = _SFXSound;
                audioSFX[i].Play();
                return;
            }
            
        }
    }

    public void stopSFXSound()
    {
    }

    public void pauseSFXSound()
    {
    }

    public void playMusicLoopSound(AudioClip _music)
    {
        audioMusic.clip = _music;
        audioMusic.loop = true;
        audioMusic.Play();
    }

    public void stopMusicLoopSound()
    {
        audioMusic.Stop();
    }

    public void setSFXVolume(float _volume)
    {
        for (int i = 0; i < audioSFX.Length; i++)
        {
            audioSFX[i].volume = _volume;
        }
    }

    public void setMusicVolume(float _volume)
    {
        
            audioMusic.volume = _volume;
        
    }
}
