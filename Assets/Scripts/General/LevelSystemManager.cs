using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class LevelSystemManager : MonoBehaviour
{
    [SerializeField] public Level_Bar levelBar;
    [SerializeField] public UI_SkillTree shop;
    [SerializeField] public PlayerMovement player;
    [SerializeField] public PlayerData data;
    [SerializeField] public GameObject manager;
    private List<EnemyHealth> enemyHealthList = new List<EnemyHealth>();
    private int enemyIndex = 0;

    private void Awake()
    {
        LevelSystem levelSystem = new LevelSystem();
        levelBar.SetLevelSystem(levelSystem);
        shop.SetLevelSystem(levelSystem);


        foreach (Transform child in manager.transform)
        {
            enemyHealthList.Add(child.GetComponent<EnemyHealth>());
        }

        foreach (var item in enemyHealthList)
        {
            item.SetLevelSystem(levelSystem);
        }

        LevelSystemAnimated levelSystemAnimated = new LevelSystemAnimated(levelSystem);
        levelBar.SetLevelSystemAnimated(levelSystemAnimated);
        player.SetLevelSystemAnimated(levelSystemAnimated);
    }
}

