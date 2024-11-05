using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeController : MonoBehaviour
{

    public Slider slider;

    public void setMaxLife(int life)
    {
        slider.maxValue = life;
        slider.value = life;
    }

    public void SetLife(int life)
    {
        slider.value = life;
    }



    
}
