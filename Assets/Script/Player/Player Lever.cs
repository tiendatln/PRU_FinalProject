using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLever : MonoBehaviour
{
    public TextMeshProUGUI TextMeshProUGUI;
    public Slider leverSlider;

    private void Update()
    {
        leverSlider.value = GameManager.Instance.GetPlayerData().leverEX;
        TextMeshProUGUI.text = GameManager.Instance.GetPlayerData().leverText.ToString();
    }

    public virtual void TakeLever(int EX)
    {
        GameManager.Instance.GetPlayerData().leverEX += EX;
        if (GameManager.Instance.GetPlayerData().leverText < 20)
        {
            GameManager.Instance.GetPlayerData().leverUP();
        }
        
    }

}
