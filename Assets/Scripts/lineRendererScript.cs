using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class lineRendererScript : MonoBehaviour
{
    private Transform[] points;
    private LineRenderer lineRend;

    private void Awake()
    {
        lineRend = GetComponent<LineRenderer>();
    }

    void Start()
    {
        lineRend.positionCount = 2;
    }

    public void DrawLineBetweenObjects(Transform firstT, Transform secondT)
    {
        lineRend.SetPosition(0, firstT.position);
        lineRend.SetPosition(1, secondT.position);
    }
}
