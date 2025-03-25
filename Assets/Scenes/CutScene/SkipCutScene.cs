using UnityEngine;
using UnityEngine.Playables;

public class SkipCutScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioSource setVolumeCutScene;
    private PlayableDirector playableDirector;
    private GameObject nextScene;
    void Start()
    {
        //nextScene = GameObject.FindWithTag("NextScene");
        //if (nextScene.gameObject.activeSelf == true)
        //{
        //    nextScene.gameObject.SetActive(false);
        //}
        
        Invoke("take", 0.00001f);
        AudioManager.Instance.stopMusicLoopSound();
    }

    void take()
    {
        
        setVolumeCutScene.volume = AudioManager.Instance.audioMusic.volume;
        playableDirector = GameObject.FindWithTag("CutScene").GetComponent<PlayableDirector>();
    }

    public void Skip()
    {
        playableDirector.Stop();
    }
}
