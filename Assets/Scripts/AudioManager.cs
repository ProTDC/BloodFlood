using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private GameObject[] terminals;

    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip optionSelect;
    public AudioClip buttonClicked;
    public AudioClip background;
    public AudioClip backgroundFish;
    public AudioClip backgroundAlternateFish1;
    public AudioClip backgroundAlternateFish2;
    public AudioClip shop;
    public AudioClip pause;
    public AudioClip unPause;
    public AudioClip fishCollected;
    public AudioClip hamsterCollected;
    public AudioClip bloodHittingFloor;
    public AudioClip terminalTurnOn;
    public AudioClip terminalTurnOff;
    public AudioClip playerJumping;
    public AudioClip playerLanding;
    public AudioClip playerDashing;
    public AudioClip playerShooting;
    public AudioClip playerTakingDamage;
    public AudioClip playerDying;
    public AudioClip playerSwordSwing;
    public AudioClip playerSwordFlesh;
    public AudioClip playerSwordMetal;
    public AudioClip playerBulletFlesh;
    public AudioClip playerBulletMetal;
    public AudioClip droneIdle;
    public AudioClip droneChase;
    public AudioClip droneShoot;

    public bool isFishing = false;

    [Header("Variables")]
    private bool isTerminalActive = false;
    public float loopPoint = 5f;

    private void Start()
    {
        //musicSource.clip = background;
        //musicSource.Play();

        if (isFishing == false)
        {
            OnPlayMusic(background);
        }
        else
        {
            
        }
    }

    public void OnPlayMusic(AudioClip backgroundMusic)
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    private void Update()
    {
        if (terminals != null)
        {
            if (terminals[0].activeInHierarchy == true || terminals[1].activeInHierarchy == true || terminals[2].activeInHierarchy == true && terminals != null)
            {
                isTerminalActive = true;
            }
            else
            {
                isTerminalActive = false;
            }

            if (isTerminalActive == true)
            {
                if (musicSource.clip == background)
                {
                    musicSource.clip = shop;
                    musicSource.Play();
                }
            }

            if (isTerminalActive == false)
            {
                if (musicSource.clip == shop)
                {
                    musicSource.clip = background;
                    musicSource.Play();
                }
            }
        }
        else
        {
            return;
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
