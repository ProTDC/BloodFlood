using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Flickering : MonoBehaviour
{
    [SerializeField]
    public float duration = 0f;
    public float duration1 = 0.8f;

    public float value1 = 1.4f;
    public float value2 = 0f;

    public UnityEngine.Rendering.Universal.Light2D lamp;

    void Start()
    {
        lamp = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
    }
    void Update()
    {
        lamp.intensity = Mathf.Lerp(value1, value2, Random.Range(duration, duration1));

    }
}
