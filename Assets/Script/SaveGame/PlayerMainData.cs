using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "PlayerMainData", menuName = "Scriptable Objects/PlayerMainData")]
public class PlayerMainData : ScriptableObject
{
    public float health;
    public float attack;
    public float attackSkill;
    public int leverEX;
    public int leverText;
    public float[] PlayerPosition = new float[3]; // Khởi tạo mặc định
    public GameObject StartPosition;
    public Vector3[] StartGate;
    public int Mapindex;

    public void SavePlayer(string filePath = null)
    {
        Mapindex = SceneManager.GetActiveScene().buildIndex;
        StartPosition = GameObject.Find("Character");
        SetVectorPlayer(StartPosition.transform.position);
        SaveSystem.SavePlayer(this, filePath);
    }

    public void LoadData(string filePath = null)
    {
        PlayerData data = SaveSystem.LoadPlayer(filePath);
        if (data != null)
        {
            health = data.health;
            attack = data.attack;
            attackSkill = data.attackSkill;
            leverEX = data.leverEX;
            leverText = data.leverText;
            Mapindex = data.MapIndex;

            if (data.PlayerPosition != null && data.PlayerPosition.Length >= 3)
            {
                PlayerPosition[0] = data.PlayerPosition[0];
                PlayerPosition[1] = data.PlayerPosition[1];
                PlayerPosition[2] = data.PlayerPosition[2];
            }
        }
    }

    public void NewGame(int mapIndex)
    {
        SaveSystem.DeleteSaveFile();
        CheckPointNew(mapIndex);
    }

    public void CheckPointNew(int mapIndex)
    {
        SetVectorPlayer(StartGate[mapIndex - 1] - new Vector3(0,0, 20));
    }

    public void leverUP()
    {
        if(leverEX >= 100)  
        {
            leverText += 1;
        }
    }

    public Vector3 GetVectorPLayer()
    {
        return new Vector3(PlayerPosition[0], PlayerPosition[1], PlayerPosition[2]);
    }

    public void SetVectorPlayer(Vector3 position)
    {
        PlayerPosition[0] = position.x;
        PlayerPosition[1] = position.y;
        PlayerPosition[2] = position.z;
    }
}