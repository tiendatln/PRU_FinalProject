using UnityEngine;
using UnityEngine.Playables;

public class SkipCutScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioSource setVolumeCutScene;
    private PlayableDirector playableDirector;
    void Start()
    {
        playableDirector = GameObject.FindWithTag("CutScene").GetComponent<PlayableDirector>();
        setVolumeCutScene.volume = AudioManager.Instance.audioMusic.volume;
    }

    public void Skip()
    {
        playableDirector.Stop();
    }
}
