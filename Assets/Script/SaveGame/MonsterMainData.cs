using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterMainData", menuName = "Scriptable Objects/MonsterMainData")]
public class MonsterMainData : ScriptableObject
{
    public Dictionary<GameObject, Vector3> monsterPositions = new Dictionary<GameObject, Vector3>();

}
