using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon_Equip : MonoBehaviour
{
    public Button[] weaponButtons;

    public GameObject[] playerWeapons;

    private bool EncryptionEnabled;
    private IDataService dataService = new JsonDataService();
    private WeaponClass stats = new WeaponClass();

    private void Start()
    {

        WeaponClass data = dataService.LoadData<WeaponClass>("/weapon.json", EncryptionEnabled);
        var jObject = JsonConvert.SerializeObject(data, Formatting.Indented);
        var json = JObject.Parse(jObject);

        int weaponToAdd = json.Value<int>("currentWeapon");
        EnableEntry(weaponToAdd);
        EnableWeaponEntry(weaponToAdd);
    }

    public void EnableEntry(int id)
    {
        for (int i = 0; i < weaponButtons.Length; i++)
        {
            weaponButtons[i].interactable = true;
        }
        weaponButtons[id].interactable = false;
    }

    public void EnableWeaponEntry(int id)
    {
        for (int i = 0; i < playerWeapons.Length; i++)
        {
            playerWeapons[i].SetActive(false);
        }
        playerWeapons[id].SetActive(true);
        AddToWeaponJSON(id);
        Debug.Log($"WeaponID = {id}");
    }

    public void SerializeJson()
    {
        //Checks if it already exists
        if (dataService.SaveData("/weapon.json", stats, EncryptionEnabled))
        {
            try
            {
                //Overwrite data if yes
                WeaponClass data = dataService.LoadData<WeaponClass>("/weapon.json", EncryptionEnabled);

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

    public void AddToWeaponJSON(int id)
    {
        stats.currentWeapon = id;
        SerializeJson();
    }
}
