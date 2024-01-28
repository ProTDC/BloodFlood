using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExtraMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] backgroundObjects;
    [SerializeField] private TextMeshProUGUI selectionText;

    [SerializeField] public int index;
    [SerializeField] private int indexLimit;

    private void Start()
    {
        indexLimit = backgroundObjects.Length - 1;

        index = PlayerPrefs.GetInt("background");
    }

    private void Update()
    {
        //if (index >= indexLimit)
        //{
        //    index = indexLimit;
        //}

        if (index == 0)
        {
            selectionText.text = "Ship";
            backgroundObjects[0].SetActive(true);
            PlayerPrefs.SetInt("background", 0);
            PlayerPrefs.Save();
        }
        else 
        {
            backgroundObjects[0].SetActive(false);
        }

        if (index == 1)
        {
            selectionText.text = "Planetarium";
            backgroundObjects[1].SetActive(true);
            PlayerPrefs.SetInt("background", 1);
            PlayerPrefs.Save();
        }
        else 
        {
            backgroundObjects[1].SetActive(false);
        }

        if (index > indexLimit)
        {
            index = 0;
        }

        if (index < 0)
        {
            index = indexLimit;
        }
    }

    public void NextBackground()
    {
        index++;
    }

    public void PreviousBackground()
    {
        index--;
    }
}
