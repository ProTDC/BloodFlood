using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex;

    public float waitTime = 2f;
    private bool hasSeen = false;


    private void Awake()
    {
        //if (PlayerPrefs.HasKey("Tutorial") || PlayerPrefs.GetInt("Tutorial") != 1)
        //{
        //    hasSeen = false;
        //    PlayerPrefs.SetInt("Tutorial", 1);
        //    PlayerPrefs.Save();
        //}
        //else if (PlayerPrefs.GetInt("Tutorial") == 1)
        //{
        //    hasSeen = true;
        //}
    }

    private void Start()
    {
    }

    private void Update()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                popUps[i].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }

        if (popUpIndex == 0)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 1)
        {
            if (Input.GetKeyUp(KeyCode.I))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 2)
        {
            if (Input.GetKeyUp(KeyCode.O))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 3)
        {
            if (Input.GetKeyUp(KeyCode.U))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 4)
        {
            if (Input.GetKeyUp(KeyCode.P))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 5)
        {
            Invoke("End", 5f);
        }
    }

    private void End()
    {
        popUpIndex = -1;
    }
}
