using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioClip audioClip;
    public void playmusic(){
        AudioManager.Instance.playMusicLoopSound(audioClip);


     }

    public void Start()
    {
        Invoke("playmusic", 0.00001f);
    }
}
