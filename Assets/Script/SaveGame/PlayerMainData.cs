
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "PlayerMainData", menuName = "Scriptable Objects/PlayerMainData")]
public class PlayerMainData : ScriptableObject
{
   
    public Vector3[] StartGateOfCurrentMap; // Mảng chứa vị trí của các cổng ở từng map
        
}