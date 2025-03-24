    using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class NextMap : MonoBehaviour
{

    public PlayableDirector playableDirector;
    public void nextMap()
    {
        playableDirector = GameObject.FindWithTag("CutScene").GetComponent<PlayableDirector>();

        if (SceneManager.GetActiveScene().buildIndex + 1 < 6)
        {
            GameManager.Instance.getMainData().indexOfCurrentMap = SceneManager.GetActiveScene().buildIndex + 1;

            GameManager.Instance.getMainData().setPositionNextMap(); // set vị trí của nhân vật là vị trí cổng ở map tiếp theo

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // lấy sceen hiện tại + 1 để chuyển sceen

        }
        else
        {
            playableDirector.gameObject.SetActive(true);


            // Bắt đầu Coroutine để chờ cutscene chạy xong
            StartCoroutine(PlayCutSceneAndLoadNext());
            
        }

    }
    private IEnumerator PlayCutSceneAndLoadNext()
    {
        // Chờ cho đến khi timeline kết thúc
        while (playableDirector.state == PlayState.Playing)
        {
            //playableDirector.time += Time.deltaTime; // Đặt lại về đầu

            yield return null; // Chờ mỗi frame cho đến khi timeline dừng
        }

        GameManager.Instance.getMainData().indexOfCurrentMap = 1;

        GameManager.Instance.getMainData().setPositionNextMap(); // set vị trí của nhân vật là vị trí cổng ở map tiếp theo

        SceneManager.LoadSceneAsync(0); // lấy sceen hiện tại + 1 để chuyển sceen

        // Dừng PlayableDirector (không cần thiết lắm vì scene đã chuyển, nhưng để chắc chắn)
        playableDirector.Stop();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                nextMap();
            }
        }
    }
}
