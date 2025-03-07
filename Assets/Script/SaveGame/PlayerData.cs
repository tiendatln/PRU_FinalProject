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

        public PlayerData(PlayerMainData player)
        {
            health = player?.health ?? 100f;
            attack = player?.attack ?? 2f;
            leverEX = player?.leverEX ?? 0;
            leverText = player?.leverText ?? 1;
            attackSkill = player?.attackSkill ?? 5;

            if (player != null && player.PLayerPosition != null && player.PLayerPosition.Length >= 3)
            {
                PlayerPosition = new float[3];
                PlayerPosition[0] = player.PLayerPosition[0];
                PlayerPosition[1] = player.PLayerPosition[1];
                PlayerPosition[2] = player.PLayerPosition[2];
            }
            else
            {
                PlayerPosition = null;
                Debug.LogWarning("PLayerPosition trong PlayerMainData không hợp lệ hoặc PlayerMainData là null.");
            }
        }
    }
