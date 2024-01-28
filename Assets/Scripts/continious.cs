using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class continious : MonoBehaviour
{
    private Slider slider;
    public float movementDuration = 1.0f; 

    private float timer = 0.0f;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        float normalizedTime = timer / movementDuration;
        slider.value = Mathf.Lerp(0, slider.maxValue, normalizedTime);

        if (timer >= movementDuration)
        {
            timer -= movementDuration; 
        }
    }
}
