using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class SettingSlider : MonoBehaviour
{
    public GameObject[] options;
    public Slider slider;

    private int loop;

    private void Start()
    {
        foreach (var option in options) 
        {
            option.SetActive(false);
        }

        loop = options.Length;
        slider.maxValue = loop;

    }

    private void Update()
    {
        slider.onValueChanged.AddListener((v) => {

            if (v >= 1)
                options[0].SetActive(true);
            else
                options[0].SetActive(false);

            if (v >= 2) 
                options[1].SetActive(true);
            else 
                options[1].SetActive(false);

            if (v >= 3)
                options[2].SetActive(true);
            else
                options[2].SetActive(false);

            if (v >= 4)
                options[3].SetActive(true);
            else
                options[3].SetActive(false);

            if (v >= 5)
                options[4].SetActive(true);
            else
                options[4].SetActive(false);

            if (v >= 6)
                options[5].SetActive(true);
            else
                options[5].SetActive(false);

            if (v >= 7)
                options[6].SetActive(true);
            else
                options[6].SetActive(false);

            //STANDBY
            //if (v >= 8)
            //    options[7].SetActive(true);
            //else
            //    options[7].SetActive(false);
        });
    }
}
