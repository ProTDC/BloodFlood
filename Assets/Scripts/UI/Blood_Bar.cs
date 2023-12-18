using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blood_Bar : MonoBehaviour
{
    public Slider slider;
    public Image fill;

    public void SetMaxBlood(float blood)
    {
        slider.maxValue = blood;
    }

    public void SetBlodd(float blood)
    {
        slider.value = blood;
    }
}
