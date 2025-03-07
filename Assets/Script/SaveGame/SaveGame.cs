using UnityEngine;

public class SaveGame : MonoBehaviour
{
    public PlayerLever PlayerLever;
    public PlayerMainData MainData;

    public void SaveGameBtn()
    {
        MainData.SavePlayer();
    }

    public void LoadGame()
    {
        MainData.LoadData();
    }
}
