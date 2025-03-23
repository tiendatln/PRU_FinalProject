using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    
    [SerializeField] private MainData mainData;
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
        if (!isDataLoaded && mainData != null)
        {
            mainData.LoadData();
            isDataLoaded = true;
            Debug.Log("Player data loaded once at game start.");
        }
    }

    // Truy cập PlayerMainData từ bất kỳ đâu

    public MainData getMainData()
    {
        return mainData;
    }

    // Lưu dữ liệu khi cần (ví dụ: khi thoát game)
    private void OnApplicationQuit()
    {
        if (mainData != null)
        {

            mainData.SavePlayer();
            //playerData.SaveStartGateOfCurrentMap();
            Debug.Log("Player data saved on game quit.");
        }
    }
    
}
