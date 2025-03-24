using UnityEngine;

public class AudioBackground : MonoBehaviour

{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public AudioClip bg;
    void Start()
    {
        AudioManager.Instance.playMusicLoopSound(bg);     
    }

    
}
