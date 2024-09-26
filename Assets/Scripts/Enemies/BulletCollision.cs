using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BulletCollision : MonoBehaviour
{
    private PlayerMovement player;
    private AudioManager audioManager;
    private Volume volume;
    private Bloom b;
    private ChromaticAberration c;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        volume = GameObject.FindGameObjectWithTag("GlobalVolume").GetComponent<Volume>();
        volume.profile.TryGet(out b);
        volume.profile.TryGet(out c);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") && player.CanUseDeflect())
        {
            StartCoroutine(Reflect(collision));
        }
    }

    public IEnumerator Reflect(Collider2D bullet)
    {
        audioManager.PlaySFX(audioManager.playerSwordSwing);
        Time.timeScale = 0.01f;
        b.intensity.value = 70f;
        c.intensity.value = 1;
        bullet.GetComponent<Projectile>().Flip();
        yield return new WaitForSeconds(0.001f);
        Time.timeScale = 1;
        b.intensity.value = 4.5f;
        c.intensity.value = 0;
    }
}
