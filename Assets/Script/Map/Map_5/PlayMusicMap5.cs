using UnityEngine;

public class PlayMusicMap5 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioClip BackgroundMusic;
    void Start()
    {
        Invoke("playMusic", 0.0001f);
    }

    void playMusic()
    {
        AudioManager.Instance.playMusicLoopSound(BackgroundMusic);
    }

    // Update is called once per fram
}
