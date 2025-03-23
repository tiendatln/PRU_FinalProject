using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLever : MonoBehaviour
{
    public TextMeshProUGUI TextMeshProUGUI;
    public Slider leverSlider;

    private void Update()
    {
        leverSlider.value = GameManager.Instance.getMainData().leverEX;
        TextMeshProUGUI.text = GameManager.Instance.getMainData().leverText.ToString();
    }

    public virtual void TakeLever(int EX)
    {
        GameManager.Instance.getMainData().leverEX += EX;
        if (GameManager.Instance.getMainData().leverText < 20)
        {
            GameManager.Instance.getMainData().leverUP();
        }
        
    }

}
