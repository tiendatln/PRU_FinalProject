using System.Collections.Generic;
using UnityEngine;
    using UnityEngine.SceneManagement;

    public class MainData : MonoBehaviour
    {
    public float health;
    public float attack;
    public float attackSkill;
    public int leverEX;
    public int leverText;
    public float[] PlayerPosition = new float[3];
    private GameObject StartPosition;

    public Vector3[] StartGateOfCurrentMap;
    public int indexOfCurrentMap;
    public float musicVolume;
    public float SFXVolume;
    public bool isNewGame;
    public PlayerMainData PlayerMainData;
    public void setPositionNextMap()
    {
        SetVectorPlayer(StartGateOfCurrentMap[(indexOfCurrentMap / 2) - 1]);
    }

    public void SavePlayer(string filePath = null)
    {
        indexOfCurrentMap = SceneManager.GetActiveScene().buildIndex > 0 ? SceneManager.GetActiveScene().buildIndex : indexOfCurrentMap;
        StartPosition = GameObject.Find("Character");
        if (StartPosition != null)
        {
            SetVectorPlayer(StartPosition.transform.position);
        }

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
            musicVolume = data.musicVolume;
            SFXVolume = data.SFXVolume;


            for (int i = 0; i < PlayerMainData.StartGateOfCurrentMap.Length; i++)
            {
                StartGateOfCurrentMap[i] = PlayerMainData.StartGateOfCurrentMap[i];
            }
                
                if (data.PlayerPosition != null && data.PlayerPosition.Length >= 3)
                {
                    PlayerPosition[0] = data.PlayerPosition[0];
                    PlayerPosition[1] = data.PlayerPosition[1];
                    PlayerPosition[2] = data.PlayerPosition[2];
                }
                isNewGame = false;
            }
            else
            {
                isNewGame = true;
            }
        }


        public void NewGame(int mapIndex)
        {
            SaveSystem.DeleteSaveFile(Application.persistentDataPath + "/playerData.json");
            SetDefaultData();
        }

        public void resetPlayer()
        {
            SetVectorPlayer(GameObject.Find("StartGate").transform.position - new Vector3(0, 0, 20));
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

        public void leverUP()
        {
            if (leverEX >= 100)
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
