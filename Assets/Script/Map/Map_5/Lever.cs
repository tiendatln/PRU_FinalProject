using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject Bride;

    private bool brideActive = false;
    public float LeverActive = 0.1f;

    private bool canActive;
    public GameObject cambound;
    public GameObject virtualCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Bride = GameObject.Find("BrideMission");

    }

    // Update is called once per frame
    void Update()
    {
        LeverActive -= Time.deltaTime;
        if (LeverActive > 0 && Input.GetKeyDown(KeyCode.F)){
            BrideActive();
        }

        if (brideActive && Bride.transform.position.y <= 0)
        {
            canActive = true;
            cambound.SetActive(false);
            virtualCamera.SetActive(true);
            Bride.transform.position = new Vector3(Bride.transform.position.x, Bride.transform.position.y + (Time.deltaTime * 2), 20);
        }
      
        if (Bride.transform.position.y > 0.001f && canActive)
        {
            Thread.Sleep(1000);
            cambound.SetActive(true);
            virtualCamera.SetActive(false);
            canActive = false;
        }
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log(Bride.transform.position.y);
            LeverActive = 0.1f;
        }
    }



    public void BrideActive()
    {
        brideActive = true;
    }
}