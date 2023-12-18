using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterManager : MonoBehaviour
{
    public int maxHamsters;
    public int hamstersAchived;

    private void Start()
    {
        maxHamsters = this.transform.childCount;

    }

    private void Update()
    {
        hamstersAchived = GameObject.FindGameObjectWithTag("Player").GetComponent<HamsterHandler>().hamstersOnHand;
    }
}
