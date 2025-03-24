using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance => _instance;

    public AudioSource[] audioSFX;
    public AudioSource audioMusic;

    private void Start()
    {

        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);

        setSFXVolume(GameManager.Instance.getMainData().SFXVolume);
       setMusicVolume(GameManager.Instance.getMainData().musicVolume);
    }

    public void playSFXSound(AudioClip _SFXSound)
    {
        for (int i = 0; i < audioSFX.Length; i++)
        {
            if (audioSFX[i].isPlaying == false)
            {
                audioSFX[i].PlayOneShot(_SFXSound);
                return;
            }
            else
            {
                audioSFX[i].PlayOneShot(_SFXSound);
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
            audioSFX [i].volume = _volume;
        }
        GameManager.Instance.getMainData().SFXVolume = _volume;
    }

    public void setMusicVolume(float _volume)
    {

        audioMusic.volume = _volume;
        GameManager.Instance.getMainData().musicVolume = _volume;
    }
}
