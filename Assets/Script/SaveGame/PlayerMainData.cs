using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMainData", menuName = "Scriptable Objects/PlayerMainData")]
public class PlayerMainData : ScriptableObject
{
    public float health = 100f;
    public float attack = 5f;
    public float attackSkill = 5;
    public int leverEX;
    public int leverText;
    public float[] PLayerPosition = new float[3]; // Khởi tạo mặc định
    public GameObject StartPosition;
    public MonsterMainData MonsterMainData;

    public void SavePlayer(string filePath = null)
    {

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
            if (data.PlayerPosition != null && data.PlayerPosition.Length >= 3)
            {
                PLayerPosition[0] = data.PlayerPosition[0];
                PLayerPosition[1] = data.PlayerPosition[1];
                PLayerPosition[2] = data.PlayerPosition[2];
            }
        }
    }

    public void NewGame()
    {
        SaveSystem.DeleteSaveFile();
        CheckPointNew();
    }

    public void CheckPointNew()
    {
        GameObject NewPosition = GameObject.Find("StartGate");
        SetVectorPlayer(NewPosition.transform.position - new Vector3(0,0, 20));
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
        return new Vector3(PLayerPosition[0], PLayerPosition[1], PLayerPosition[2]);
    }

    public void SetVectorPlayer(Vector3 position)
    {
        PLayerPosition[0] = position.x;
        PLayerPosition[1] = position.y;
        PLayerPosition[2] = position.z;
    }
}