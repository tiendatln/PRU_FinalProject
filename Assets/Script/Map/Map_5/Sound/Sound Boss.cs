using UnityEngine;

public class SoundBoss : MonoBehaviour
{
    public AudioClip BossMusic;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioManager.Instance.stopMusicLoopSound();
        AudioManager.Instance.playMusicLoopSound(BossMusic);
    }
}
