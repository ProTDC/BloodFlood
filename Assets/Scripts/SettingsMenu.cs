using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using System;
using UnityEngine.Rendering;
using static CodeMonkey.Utils.UI_TextComplex;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Toggle fullButton;
    [SerializeField] private Toggle effectButton;
    [SerializeField] private Toggle crtButton;
    public AudioMixer audioMixer;

    [SerializeField] private GameObject crtVolume;

    Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = $"{resolutions[i].width} x {resolutions[i].height}";
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        if (PlayerPrefs.HasKey("musicFloat"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetSFXVolume();
            PlayerPrefs.Save();
        }

        if (PlayerPrefs.HasKey("isFullscreen"))
        {
            bool isOn = Convert.ToBoolean(PlayerPrefs.GetString("isFullscreen"));
            fullButton.isOn = isOn;
        }

        if (PlayerPrefs.HasKey("effectsOn"))
        {
            bool isOn = Convert.ToBoolean(PlayerPrefs.GetString("effectsOn"));

            if (isOn == true)
            {
                GameObject.FindGameObjectWithTag("GlobalVolume").GetComponent<Volume>().weight = 1;
                effectButton.isOn = isOn;
            }
            else
            {
                GameObject.FindGameObjectWithTag("GlobalVolume").GetComponent<Volume>().weight = 0;
                effectButton.isOn = isOn;
            }
        }

        if (PlayerPrefs.HasKey("crtOn"))
        {
            bool isOn = Convert.ToBoolean(PlayerPrefs.GetString("crtOn"));

            if (isOn == true)
            {
                crtVolume.SetActive(true);
                crtButton.isOn = isOn;
            }
            else
            {
                crtVolume.SetActive(false);
                crtButton.isOn = isOn;
            }
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("musicVolume",  Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicFloat", volume);
        PlayerPrefs.Save();
    }
    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        audioMixer.SetFloat("sfxVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxFloat", volume);
        PlayerPrefs.Save();
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicFloat");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxFloat");

        SetMusicVolume();
        SetSFXVolume();
        PlayerPrefs.Save();
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetString("isFullscreen", isFullscreen.ToString());
        PlayerPrefs.Save();
    }

    public void SetEffects(bool effectsOn)
    {
        GameObject globalVolumeObject = GameObject.FindGameObjectWithTag("GlobalVolume");
        if (globalVolumeObject != null)
        {
            Volume globalVolume = globalVolumeObject.GetComponent<Volume>();
            if (globalVolume != null)
            {
                globalVolume.weight = effectsOn ? 1 : 0;
                PlayerPrefs.SetString("effectsOn", effectsOn.ToString());
                PlayerPrefs.Save();
            }
            else
            {
                Debug.LogError("Volume component not found on the object with 'GlobalVolume' tag.");
            }
        }
        else
        {
            Debug.LogError("GameObject with 'GlobalVolume' tag not found in the scene.");
        }
    }

    public void SetCRT(bool crtOn)
    {
        if (crtVolume != null)
        {
            crtVolume.SetActive(crtOn);
            PlayerPrefs.SetString("crtOn", crtOn.ToString());
            PlayerPrefs.Save();
        }
    }
}
