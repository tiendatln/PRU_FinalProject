using System.Collections.Generic;
using UnityEngine;

public class MonsterSystem : MonoBehaviour
{
    public MonsterMainData MonsterMainData;
    // Dictionary để lưu vị trí của từng con quái
    public Dictionary<string, Vector3> monsterPositions = new Dictionary<string, Vector3>();

    // Danh sách các con quái trong scene
    public GameObject[] monsters;

    void Start()
    {
        // Khởi tạo vị trí ban đầu cho từng con quái
        InitializeMonsters();
    }

    void Update()
    {
        // Cập nhật vị trí của các con quái mỗi frame
        UpdateMonsterPositions();
    }

    void InitializeMonsters()
    {
        // Gán vị trí ban đầu cho từng con quái
        for (int i = 0; i < monsters.Length; i++)
        {
            if (monsters[i] != null)
            {
                string monsterId = "Monster_" + i; // Tạo ID duy nhất cho mỗi con quái
                monsterPositions[monsterId] = monsters[i].transform.position;
            }
        }
    }

    void UpdateMonsterPositions()
    {
        // Cập nhật vị trí mới nhất của từng con quái
        for (int i = 0; i < monsters.Length; i++)
        {
            if (monsters[i] != null)
            {
                string monsterId = "Monster_" + i;
                monsterPositions[monsterId] = monsters[i].transform.position;
            }
        }
    }

    // Hàm để lấy vị trí của một con quái cụ thể
    public Vector3 GetMonsterPosition(string monsterId)
    {
        if (monsterPositions.ContainsKey(monsterId))
        {
            return monsterPositions[monsterId];
        }
        return Vector3.zero; // Trả về vị trí mặc định nếu không tìm thấy
    }

    // Hàm để thiết lập vị trí cho một con quái
    public void SetMonsterPosition(string monsterId, Vector3 newPosition)
    {
        if (monsterPositions.ContainsKey(monsterId))
        {
            monsterPositions[monsterId] = newPosition;
            // Cập nhật vị trí thực tế trong scene
            int index = int.Parse(monsterId.Split('_')[1]);
            if (monsters[index] != null)
            {
                monsters[index].transform.position = newPosition;
            }
        }
    }

    // Hiển thị vị trí của tất cả quái vật (dùng để debug)
    public void PrintAllPositions()
    {
        foreach (var monster in monsterPositions)
        {
            Debug.Log($"{monster.Key} position: {monster.Value}");
        }
    }
}
