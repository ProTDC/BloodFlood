using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HamsterHandler : MonoBehaviour
{
    public int hamstersOnHand;
    private int hamstersOnAllTime;

    private bool EncryptionEnabled;
    private IDataService dataService = new JsonDataService();
    private HamsterClass stats = new HamsterClass();

    private TextMeshProUGUI hamsterText;

    private void Awake()
    {
        //find the UI hamster text
        hamsterText = GameObject.FindGameObjectWithTag("hamsterText").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        //Insansiate hamsters from Json or something idk
        HamsterClass data = dataService.LoadData<HamsterClass>("/hamsters.json", EncryptionEnabled);
        var jObject = JsonConvert.SerializeObject(data, Formatting.Indented);
        var json = JObject.Parse(jObject);

        //What the fuck
        int hamstersToAdd = json.Value<int>("TotalHamsters");
        hamstersOnAllTime += hamstersToAdd;

        //Set the UI hamster text to the current number of hamsters held by player
        hamsterText.text = hamstersToAdd.ToString();
    }

    //Uses arcane and ancient techniques to save the current number of hamsters into a Json
    public void SerializeJson()
    {
        //Checks if it already exists
        if (dataService.SaveData("/hamsters.json", stats, EncryptionEnabled))
        {
            try
            {
                //Overwrite data if yes
                HamsterClass data = dataService.LoadData<HamsterClass>("/hamsters.json", EncryptionEnabled);

            }
            catch (Exception ex)
            {
                //God fucking damnit something broke again
                Debug.LogError($"Could not read file!");
            }
        }
        else
        {
            //God fucking damn it something broke again
            Debug.LogError("Could Not save file!");
        }
    }

    public void AddToHamsters()
    {
        stats.TotalHamsters = hamstersOnHand;
        SerializeJson();
        hamsterText.text = hamstersOnHand.ToString();
    }
}
