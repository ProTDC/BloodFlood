using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerMenu : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip clickingButton;
    public AudioClip optionEnded;

    [Header("Music")]
    public AudioClip MainTheme;

    public AudioClip[] menuThemes;

    private void Start()
    {
        musicSource.clip = menuThemes[PlayerPrefs.GetInt("MenuMusic")];
        musicSource.Play();
    }

    public void ChangeMusic(int musicIndex)
    {
        musicSource.Stop();
        musicSource.clip = menuThemes[musicIndex];
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayClick()
    {
        sfxSource.PlayOneShot(clickingButton);
    }

}
