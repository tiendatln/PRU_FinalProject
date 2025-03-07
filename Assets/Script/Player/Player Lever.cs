using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLever : MonoBehaviour
{
    public PlayerMainData PlayerMainData;
    public TextMeshProUGUI TextMeshProUGUI;
    public Slider leverSlider;

    private void Update()
    {
        leverSlider.value = PlayerMainData.leverEX;
        TextMeshProUGUI.text = PlayerMainData.leverText.ToString();
    }

    public virtual void TakeLever(int EX)
    {
        PlayerMainData.leverEX += EX;
        if (PlayerMainData.leverText < 20)
        {
            PlayerMainData.leverUP();
        }
        
    }

}
