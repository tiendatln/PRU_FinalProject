using UnityEngine;

public class AudioMap6 : MonoBehaviour
{
    public AudioClip bg;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.Instance.playMusicLoopSound(bg);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
