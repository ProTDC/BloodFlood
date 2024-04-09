using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExtraMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] backgroundObjects;
    [SerializeField] private TextMeshProUGUI selectionText;

    [SerializeField] public int backgroundIndex;
    [SerializeField] private int indexLimit;

    private AudioManagerMenu audioManager;
    [SerializeField] private TextMeshProUGUI selectionTextMusic;

    [SerializeField] public int musicIndex;
    [SerializeField] private int musicIndexLimitMax;

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManagerMenu>();
        musicIndexLimitMax = audioManager.menuThemes.Length - 1;

        indexLimit = backgroundObjects.Length - 1;
        backgroundIndex = PlayerPrefs.GetInt("background");
    }

    private void Update()
    {
        if (backgroundIndex == 0)
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

        if (backgroundIndex == 1)
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

        if (backgroundIndex == 2)
        {
            selectionText.text = "Infection";
            backgroundObjects[2].SetActive(true);
            PlayerPrefs.SetInt("background", 2);
            PlayerPrefs.Save();
        }
        else
        {
            backgroundObjects[2].SetActive(false);
        }

        if (backgroundIndex > indexLimit)
        {
            backgroundIndex = 0;
        }

        if (backgroundIndex < 0)
        {
            backgroundIndex = indexLimit;
        }

        if (musicIndex > musicIndexLimitMax)
        {
            musicIndex = 0;
        }

        if (musicIndex < 0)
        {
            musicIndex = musicIndexLimitMax;
        }

        switch (musicIndex)
        {
            case 0:
                selectionTextMusic.text = "MainTheme";
                PlayerPrefs.SetInt("MenuMusic", 0);
                PlayerPrefs.Save();
                break;

            case 1:
                selectionTextMusic.text = "Ship";
                PlayerPrefs.SetInt("MenuMusic", 1);
                PlayerPrefs.Save();
                break;

            case 2:
                selectionTextMusic.text = "Infection";
                PlayerPrefs.SetInt("MenuMusic", 2);
                PlayerPrefs.Save();
                break;

            case 3:
                selectionTextMusic.text = "El_Espanol";
                PlayerPrefs.SetInt("MenuMusic", 3);
                PlayerPrefs.Save();
                break;

            case 4:
                selectionTextMusic.text = "Natarja";
                PlayerPrefs.SetInt("MenuMusic", 4);
                PlayerPrefs.Save();
                break;

            case 5:
                selectionTextMusic.text = "Shiva";
                PlayerPrefs.SetInt("MenuMusic", 5);
                PlayerPrefs.Save();
                break;
        }
    }

    public void NextBackground()
    {
        backgroundIndex++;
    }

    public void PreviousBackground()
    {
        backgroundIndex--;
    }

    public void NextSong()
    {
        musicIndex++;

        if (musicIndex > musicIndexLimitMax)
        {
            audioManager.ChangeMusic(0);
        }
        else
        {
            audioManager.ChangeMusic(musicIndex);
        }
    }

    public void PreviousSong()
    {
        musicIndex--;

        if (musicIndex < 0)
        {
            audioManager.ChangeMusic(musicIndexLimitMax);
        }
        else
        {
            audioManager.ChangeMusic(musicIndex);
        }
    }
}
