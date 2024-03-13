using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject[] entries;

    public GameObject[] weaponEntries;

    public void EnableEntry(int id)
    {
        for (int i = 0; i < entries.Length; i++)
        {
            entries[i].SetActive(false);
        }
        entries[id].SetActive(true);
    }

    public void EnableWeaponEntry(int id)
    {
        for (int i = 0; i < weaponEntries.Length; i++)
        {
            weaponEntries[i].SetActive(false);
        }
        weaponEntries[id].SetActive(true);
    }
}
