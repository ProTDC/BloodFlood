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
        hamsterText = GameObject.FindGameObjectWithTag("hamsterText").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        HamsterClass data = dataService.LoadData<HamsterClass>("/hamsters.json", EncryptionEnabled);
        var jObject = JsonConvert.SerializeObject(data, Formatting.Indented);
        var json = JObject.Parse(jObject);

        int hamstersToAdd = json.Value<int>("TotalHamsters");
        hamstersOnAllTime += hamstersToAdd;

        hamsterText.text = hamstersToAdd.ToString();
    }

    public void SerializeJson()
    {
        if (dataService.SaveData("/hamsters.json", stats, EncryptionEnabled))
        {
            try
            {
                HamsterClass data = dataService.LoadData<HamsterClass>("/hamsters.json", EncryptionEnabled);

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

    public void AddToHamsters()
    {
        stats.TotalHamsters = hamstersOnHand;
        SerializeJson();
        hamsterText.text = hamstersOnHand.ToString();
    }
}
