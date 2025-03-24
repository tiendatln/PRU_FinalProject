using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutSceneMap2 : MonoBehaviour
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadSceneAsync(nextSceneIndex);

        // Gọi NewGame với chỉ số scene mới
        GameManager.Instance.getMainData().NewGame(nextSceneIndex);

        GameManager.Instance.getMainData().setPositionNextMap();

    }
}
    