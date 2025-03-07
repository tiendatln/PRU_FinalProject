using UnityEngine;
using System.IO;

public static class SaveSystem
{
    private static string defaultSavePath = Application.persistentDataPath + "/playerData.json";

    public static void SavePlayer(PlayerMainData player, string filePath = null)
    {
        string path = filePath ?? defaultSavePath;
        PlayerData data = new PlayerData(player);
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
        Debug.Log("Saved to: " + path);
    }

    public static PlayerData LoadPlayer(string filePath = null)
    {
        string path = filePath ?? defaultSavePath;
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            if (!string.IsNullOrEmpty(json))
            {
                return JsonUtility.FromJson<PlayerData>(json);
            }
        }
        return null;
    }
    public static void DeletePlayerSave(string filePath = null)
    {
        string path = filePath ?? defaultSavePath;
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Deleted save file at: " + path);
        }
        else
        {
            Debug.Log("No save file found at: " + path);
        }
    }
}