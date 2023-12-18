using CodeMonkey.Utils;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Fishstiary : MonoBehaviour
{
    private IDataService dataService = new JsonDataService();
    private FishStats stats = new FishStats();
    private bool EncryptionEnabled;
    public GameObject[] entries;
    public Button[] buttons;

    private void Start()
    {
        FishStats data = dataService.LoadData<FishStats>("/fish-stats.json", EncryptionEnabled);
        var jObject = JsonConvert.SerializeObject(data, Formatting.Indented);
        var json = JObject.Parse(jObject);

        for (int i = 0; i < entries.Length; i++)
        {
            var fish = json["FishDict"][$"{i}"];

            if (fish.Value<bool>() == true)
            {
                buttons[i].interactable = true;
            }
            else
            {
                buttons[i].interactable = false;
            }
        }

    }

    public void EnableEntryButton(int id)
    {
        buttons[id].interactable = true;
        AddFish(id, true);

        SerializeJson();
    }

    public void EnableEntry(int id)
    {
        for (int i = 0; i < entries.Length; i++)
        {
            entries[i].SetActive(false);
        }
        entries[id].SetActive(true);
    }

    public void SerializeJson()
    {
        if (dataService.SaveData("/fish-stats.json", stats, EncryptionEnabled))
        {
            try
            {
                FishStats data = dataService.LoadData<FishStats>("/fish-stats.json", EncryptionEnabled);

            }
            catch (Exception ex) 
            {
                Debug.LogError($"Could not read file!");
            }
        }
        else
        {
            Debug.LogError("Could Not save file!");
        }
    }

    private void AddFish(int id, bool value)
    {
        if (stats.FishDict.ContainsKey(id))
        {
            stats.FishDict[id] = value;
        }
        else
        {
            stats.FishDict.Add(id, value);
        }
    }
}
