using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MainMeny : MonoBehaviour
{
    private IDataService dataService = new JsonDataService();
    public LevelLoader LevelLoader;
    private bool EncryptionEnabled;
    public ExtraMenu extra;

    public void PlayGame()
    {
        LevelLoader.LoadNextLevel();
    }

    public void PlayFishing()
    {
        LevelLoader.LoadFishingLevel();
    }

    public void PlayPlanet()
    {
        LevelLoader.LoadPlanetLevel();
    }

    public void PlaySingle(int id)
    {
        LevelLoader.LoadSingleLevel(id);
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
        extra.index = 0;
        dataService.DeleteData<string>("/fish-stats.json");
        dataService.DeleteData<string>("/hamsters.json");
        dataService.DeleteData<string>("/points.json");
        dataService.DeleteData<string>("/level-stats.json");
    }
}
