using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class barChange : MonoBehaviour
{
    public Slider slider;

    public void maxHealth(float temp)
    {
        slider.maxValue = temp;
    }
    public void sliderUpdate(float health)
    {
        slider.value = health; 
    }
   
}
