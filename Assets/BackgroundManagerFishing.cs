using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class BackgroundManagerFishing : MonoBehaviour
{
    [SerializeField] public Light2D sunLight;
    private AudioManager audioManager;

    [SerializeField] public GameObject[] dayTime;
    [SerializeField] public GameObject[] nightTime;
    [SerializeField] public GameObject[] rainTime;

    public int rndNumber;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        audioManager.isFishing = true;
        rndNumber = Random.Range(0, 2);

        switch (rndNumber) 
        { 
            case 0:
                audioManager.OnPlayMusic(audioManager.backgroundFish);
                sunLight.intensity = 1f;
                foreach (GameObject go in dayTime) 
                {
                    go.SetActive(true);
                }
                break;

            case 1:
                audioManager.OnPlayMusic(audioManager.backgroundAlternateFish1);
                sunLight.intensity = .2f;
                foreach (GameObject go in nightTime)
                {
                    go.SetActive(true);
                }
                break;

            case 2:
                audioManager.OnPlayMusic(audioManager.backgroundAlternateFish2);
                sunLight.intensity = .6f;
                foreach (GameObject go in rainTime)
                {
                    go.SetActive(true);
                }
                break;
        }
    }
}
