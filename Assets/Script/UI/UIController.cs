using UnityEngine;

public class UIController : MonoBehaviour
{

    private static UIController _instance;
    public static UIController Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UIController instance is null! Ensure there is a UIController in the scene.");
            }
            return _instance;
        }
    }

    [SerializeField] private GameObject deadMenu;
    [SerializeField] private GameObject PlayerUI;
    [SerializeField] private GameObject PauseMenu;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Debug.LogWarning("Multiple UIController instances found! Destroying the new one.");
            Destroy(gameObject);
            return;
        }
        _instance = this;

        deadMenu = GameObject.FindWithTag("DeadUI");
        PlayerUI = GameObject.Find("Player UI");
        PauseMenu = GameObject.FindWithTag("PauseMenu");
        deadMenu.SetActive(false);
        PauseMenu.SetActive(false);
    }

    public GameObject GetDeadMenu() => deadMenu;
    public GameObject GetPlayerUI() => PlayerUI;
    public GameObject GetPauseMenu() => PauseMenu;

}
