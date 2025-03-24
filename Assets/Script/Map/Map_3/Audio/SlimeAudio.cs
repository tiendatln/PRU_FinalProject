using UnityEngine;

public class SlimeAudio : MonoBehaviour
{
    public AudioClip slimeStep;

    public void PlaySlimeStep()
    {
        AudioManager.Instance.playMusicLoopSound(slimeStep);
    }
}
