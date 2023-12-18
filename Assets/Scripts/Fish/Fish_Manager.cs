using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_Manager : MonoBehaviour
{
    [SerializeField]
    public GameObject[] fish;

    public Dictionary<string, float> spawnChances = new Dictionary<string, float>()
    {
        {"Can Fish", 0.6f},
        {"Giant Eye", 0.6f},
        {"Killer Fish", 0.6f},
        {"Lil Fish", 0.6f},
        {"Sea Bass", 0.6f},
        {"Tiny Fish", 0.6f},
        {"Unalive Fish", 0.6f},
        {"Water Fish", 0.6f},
        {"Frog", 0.6f}
    };
}
