using UnityEngine;

public class AudioBackground : MonoBehaviour
{

    public AudioClip backgroundSound;

    void Start()
    {
        Invoke("RunBackgroundSound", 0.2f);
    }

    private void RunBackgroundSound()
    {
        AudioManager.Instance.playMusicLoopSound(backgroundSound);
    }
}
