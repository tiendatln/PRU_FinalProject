using UnityEngine;

public class AudioBackGround : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioClip bg;

    void Start()
    {
       Invoke("playMusic", 0.000001f);
    }

    public void playMusic()
    {
        AudioManager.Instance.playMusicLoopSound(bg);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
