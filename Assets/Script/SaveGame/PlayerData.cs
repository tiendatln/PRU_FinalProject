using System.Collections.Generic;
using UnityEngine;



    [System.Serializable]
    public class PlayerData
    {
        public float health;
        public float attack;
        public float attackSkill;
        public int leverEX;
        public int leverText;
        public float[] PlayerPosition;
        public int MapIndex;
    public PlayerData(PlayerMainData player)
        {
            health = player.health;
            attack = player.attack;
            leverEX = player.leverEX;
            leverText = player.leverText;
            attackSkill = player.attackSkill;
            MapIndex = player.Mapindex;


            if (player != null && player.PlayerPosition != null && player.PlayerPosition.Length >= 3)
            {
                PlayerPosition = new float[3];
                PlayerPosition[0] = player.PlayerPosition[0];
                PlayerPosition[1] = player.PlayerPosition[1];
                PlayerPosition[2] = player.PlayerPosition[2];
            }
            else
            {
                PlayerPosition = null;
                Debug.LogWarning("PLayerPosition trong PlayerMainData không hợp lệ hoặc PlayerMainData là null.");
            }
        }
    }
