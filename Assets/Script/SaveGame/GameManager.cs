using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    [SerializeField] private PlayerMainData playerData;  // Gắn ScriptableObject trong Inspector
    private bool isDataLoaded = false;

    private void Awake()
    {
        // Singleton logic
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);

        // Load dữ liệu chỉ một lần
        if (!isDataLoaded && playerData != null)
        {
            playerData.LoadData();
            isDataLoaded = true;
            Debug.Log("Player data loaded once at game start.");
        }
    }

    // Truy cập PlayerMainData từ bất kỳ đâu
    public PlayerMainData GetPlayerData()
    {
        return playerData;
    }

    // Lưu dữ liệu khi cần (ví dụ: khi thoát game)
    private void OnApplicationQuit()
    {
        if (playerData != null)
        {
            playerData.SavePlayer();
            Debug.Log("Player data saved on game quit.");
        }
    }
    
}
