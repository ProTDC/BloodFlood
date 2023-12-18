using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodCollision : MonoBehaviour
{
    private AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("BloodParticle"))
        {
            SplatController.instance.MakeSplat(other.transform.position);
            
            float soundChance = Random.value;

            if (soundChance <= 0.12f)
            {
                audioManager.PlaySFX(audioManager.bloodHittingFloor);
            }
        }
    }
}
