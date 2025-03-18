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

    private GameObject StartPosition;

    public Vector3[] StartGateOfCurrentMap; // Mảng chứa vị trí của các cổng ở từng map
    public int indexOfCurrentMap; // map hiện tại của player đang chơi

    public void setPositionNextMap()
    {
        SetVectorPlayer(StartGateOfCurrentMap[indexOfCurrentMap - 1]); // indexOfCurrentMap - 1 -> bắt đầu từ screen 1 nhưng vị trí của các cổng ở từng map từ "0"
    }

    public void SavePlayer(string filePath = null)
    {
        indexOfCurrentMap = SceneManager.GetActiveScene().buildIndex;
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
            indexOfCurrentMap = data.MapIndex;

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
        SetDefaultData();
        CheckPointNew(mapIndex);

    }
    void SetDefaultData()
    {
        health = 100f;
        attack = 2f;
        attackSkill = 5f;
        leverEX = 0;
        leverText = 1;
        indexOfCurrentMap = 1;
    }

    public void CheckPointNew(int mapIndex)
    {
        SetVectorPlayer(StartGateOfCurrentMap[mapIndex - 1] - new Vector3(0,0, 20));
    }

    public void leverUP()
    {
        if(leverEX >= 100)  
        {
            leverText += 1;
            leverEX = 0;
            attack += (leverText / 10);
            attackSkill += (leverText / 10);
        }
    }
    public void Heal(int heal)
    {
        if (health < 100)
        {
            health += heal;
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