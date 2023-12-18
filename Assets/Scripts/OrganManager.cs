using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganManager : MonoBehaviour
{
    public static OrganManager instance;

    public GameObject[] organs;

    private int organOrder;

    private void Awake()
    {
        instance = this;
    }

    public void MakeOrgan(Vector3 position)
    {
        Vector3 spawnRot = new Vector3(0f, 0f, Random.Range(0, 360));
        var newOrgan = Instantiate(organs[Random.Range(0, organs.Length)], position, Quaternion.Euler(spawnRot)).GetComponent<SpriteRenderer>();

        organOrder++;

        newOrgan.sortingOrder = organOrder;
    }
}
