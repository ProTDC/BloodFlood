using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatController : MonoBehaviour
{
    public static SplatController instance;

    public GameObject[] splats;

    private int splatOrder;

    private void Awake()
    {
        instance = this;
    }

    public void MakeSplat(Vector3 position)
    {
        Vector3 spawnRot = new Vector3(0f, 0f, Random.Range(0, 360));
        var newSplat = Instantiate(splats[Random.Range(0, splats.Length)], position, Quaternion.Euler(spawnRot)).GetComponent<SpriteRenderer>();

        //splatOrder--;

        //newSplat.sortingOrder = splatOrder;

    }
}
