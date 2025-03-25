using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutSceneMap2 : MonoBehaviour
{


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 13)
        {
            GameManager.Instance.getMainData().indexOfCurrentMap = SceneManager.GetActiveScene().buildIndex + 1;

            GameManager.Instance.getMainData().setPositionNextMap(); // set vị trí của nhân vật là vị trí cổng ở map tiếp theo

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // lấy sceen hiện tại + 1 để chuyển sceen

        }
        else
        {

            GameManager.Instance.getMainData().NewGame();

            GameManager.Instance.getMainData().indexOfCurrentMap = 2;

            GameManager.Instance.getMainData().setPositionNextMap(); // set vị trí của nhân vật là vị trí cổng ở map tiếp theo

            SceneManager.LoadSceneAsync(0);

        }

    }
}
    