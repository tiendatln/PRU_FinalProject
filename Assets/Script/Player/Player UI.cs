using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Slider Slider;



    public virtual void SetHeath(float heath)
    {
        Slider.value = heath;
    }
}
