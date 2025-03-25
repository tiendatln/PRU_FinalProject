using UnityEngine;

public class AudioBackgroundMap1 : MonoBehaviour

{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public AudioClip bg;
    void Start()
    {
        Invoke("play", 0.00001f);
    }


    void play()
    {
        AudioManager.Instance.playMusicLoopSound(bg);
    }
    
}
