using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterMainData", menuName = "Scriptable Objects/MonsterMainData")]
public class MonsterMainData : ScriptableObject
{
    public Dictionary<string, Vector3> monsterPositions = new Dictionary<string, Vector3>();


    public GameObject Monster1;
    public GameObject Monster2;
    public GameObject Monster3;
    public GameObject Monster4;
}
