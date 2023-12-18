using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Flickering : MonoBehaviour
{
    [SerializeField]
    public float duration = 0f;
    public float duration1 = 0.8f;

    public float value1 = 1.4f;
    public float value2 = 0f;

    public Light2D lamp;

    void Start()
    {
        lamp = GetComponent<Light2D>();
    }
    void Update()
    {
        lamp.intensity = Mathf.Lerp(value1, value2, Random.Range(duration, duration1));

    }
}
