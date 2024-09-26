using CodeMonkey.Utils;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class UI_SkillTree : MonoBehaviour
{
    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;

    private PlayerSkills playerSkills;
    private LevelSystem levelSystem;

    private bool EncryptionEnabled;
    private IDataService dataService = new JsonDataService();
    private PointClass stats = new PointClass();

    private int points;
    [SerializeField] private TextMeshProUGUI pointsText;

    [SerializeField] private TextMeshProUGUI notEnoughPointsText;
    [SerializeField] private Level_Bar levelBar;
    [SerializeField] private GameObject deflectYes;
    [SerializeField] private GameObject bloodSwordYes;
    [SerializeField] private TextMeshProUGUI statusTextDeflect;
    [SerializeField] private TextMeshProUGUI statusTextBloodSword;

    [SerializeField] private GameObject DFObject;
    [SerializeField] private GameObject BSObject;
    [SerializeField] private GameObject WHObject;

    [SerializeField] private GameObject DFPurchase;
    [SerializeField] private GameObject BSPurchase;
    [SerializeField] private GameObject WHPurchase;

    private void Start()
    {
        PointClass data = dataService.LoadData<PointClass>("/points.json", EncryptionEnabled);
        var jObject = JsonConvert.SerializeObject(data, Formatting.Indented);
        var json = JObject.Parse(jObject);

        int pointsToAdd = json.Value<int>("Point");
        points += pointsToAdd;
        pointsText.text = points.ToString();
        Debug.Log(points);
    }

    private void Update()
    {
        //Ok so, checks if the yes purchase button is being clicked
        deflectYes.GetComponent<Button_UI>().ClickFunc = () => 
        {
            //Checks if the players has enough points to unlock said skill
            if (points >= playerSkills.skillCosts["Deflect"])
            {
                //Unlocks skill and subtracts points
                playerSkills.UnlockSkill(PlayerSkills.SkillType.Deflect);
                GameObject.Find("YouSure").SetActive(false);
                GameObject.Find("Deflect_Yes").SetActive(false);
                GameObject.Find("No").SetActive(false);
                statusTextDeflect.text = "UNLOCKED";

                points -= playerSkills.skillCosts["Deflect"];
                AddToStats();
            }
            else
            {
                //Dark wizard magic that disallows you to purchase skill

                if (DFPurchase != null)
                {
                    if (!DFPurchase.activeSelf)
                    {
                        DFPurchase.SetActive(true);
                    }
                }
                else
                {
                    Debug.LogWarning("DFPurchase not found!");
                }
                notEnoughPointsText.gameObject.SetActive(true);
                DFObject.SetActive(false);
                GameObject.Find("YouSure").SetActive(false);
                GameObject.Find("Deflect_Yes").SetActive(false);
                GameObject.Find("No").SetActive(false);
                GameObject.Find("DFPurchase").SetActive(true);
            }
        };

        bloodSwordYes.GetComponent<Button_UI>().ClickFunc = () =>
        {
            if (points >= playerSkills.skillCosts["BloodSword"])
            {
                playerSkills.UnlockSkill(PlayerSkills.SkillType.BloodSword);
                GameObject.Find("YouSure").SetActive(false);
                GameObject.Find("BS_Yes").SetActive(false);
                GameObject.Find("No").SetActive(false);
                statusTextBloodSword.text = "UNLOCKED";

                points -= playerSkills.skillCosts["BloodSword"];
                AddToStats();
            }
            else
            {
                if (BSPurchase != null)
                {
                    if (!BSPurchase.activeSelf)
                    {
                        BSPurchase.SetActive(true);
                    }
                }
                else
                {
                    Debug.LogWarning("BSPurchase not found!");
                }
                notEnoughPointsText.gameObject.SetActive(true);
                BSObject.SetActive(false);
                GameObject.Find("YouSure").SetActive(false);
                GameObject.Find("BS_Yes").SetActive(false);
                GameObject.Find("No").SetActive(false);
            }
        };

        pointsText.text = points.ToString();
    }

    public void SetPlayerSkills(PlayerSkills playerSkills)
    {
        this.playerSkills = playerSkills;
    }

    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;

        this.levelSystem.OnLevelChanged += LevelSystem_OnLevelCanged;
    }

    private void LevelSystem_OnLevelCanged(object sender, EventArgs e)
    {
        points += 2;
        AddToStats();
    }

    public void SerializeJson()
    {
        if (dataService.SaveData("/points.json", stats, EncryptionEnabled))
        {
            try
            {
                PointClass data = dataService.LoadData<PointClass>("/points.json", EncryptionEnabled);

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
        stats.Point = points;
        SerializeJson();
    }
}
