using CodeMonkey.Utils;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_Bar : MonoBehaviour
{
    private Text levelText;
    private Slider experienceSlider;
    private LevelSystem levelSystem;
    private LevelSystemAnimated levelSystemAnimated;

    private bool EncryptionEnabled;
    private IDataService dataService = new JsonDataService();
    private LevelSystemStats stats = new LevelSystemStats();

    private void Awake()
    {
        levelText = transform.Find("levelText").GetComponent<Text>();
        experienceSlider = GetComponent<Slider>();
    }
    private void Start()
    {
        SetLevelNumber(levelSystemAnimated.GetLevelNumber());

        LevelSystemStats data = dataService.LoadData<LevelSystemStats>("/level-stats.json", EncryptionEnabled);
        var jObject = JsonConvert.SerializeObject(data, Formatting.Indented);
        var json = JObject.Parse(jObject);

        int levelToAdd = json.Value<int>("Level");
        int expToAdd = json.Value<int>("Experience");

        if (json.Value<int>("Level") != 0)
        {
            levelSystem.SetLevelNumber(levelToAdd);
            SetLevelNumber(levelSystem.GetLevelNumber());
        }

        if (json.Value<int>("Experience") != 0)
        {
            levelSystem.SetExperienceNumber(expToAdd);
            SetExperienceBarSize(levelSystem.GetExperienceNormalized());
        }
    }

    private void SetExperienceBarSize(float experienceNormalized)
    {
        experienceSlider.value = experienceNormalized;
    }

    private void SetLevelNumber(int levelNumber)
    {
        levelText.text = $"{levelNumber}";
    }

    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;
    }

    public void SetLevelSystemAnimated(LevelSystemAnimated levelSystemAnimated)
    {
        this.levelSystemAnimated = levelSystemAnimated;

        SetLevelNumber(levelSystemAnimated.GetLevelNumber());
        SetExperienceBarSize(levelSystemAnimated.GetExperienceNormalized());

        levelSystemAnimated.OnExperienceChanged += LevelSystemAnimated_OnExperienceChanged;
        levelSystemAnimated.OnLevelChanged += LevelSystemAnimated_OnLevelChanged;

    }

    private void LevelSystemAnimated_OnLevelChanged(object sender, EventArgs e)
    {
        SetLevelNumber(levelSystemAnimated.GetLevelNumber());
        AddToStats();
    }

    private void LevelSystemAnimated_OnExperienceChanged(object sender, EventArgs e)
    {
        SetExperienceBarSize(levelSystemAnimated.GetExperienceNormalized());
    }

    public void SerializeJson()
    {
        if (dataService.SaveData("/level-stats.json", stats, EncryptionEnabled))
        {
            try
            {
                LevelSystemStats data = dataService.LoadData<LevelSystemStats>("/level-stats.json", EncryptionEnabled);

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

    private void AddToStats()
    {
        stats.Level = levelSystem.GetLevelNumber();
        stats.Experience = levelSystem.GetExperience();

        SerializeJson();
    }
}
