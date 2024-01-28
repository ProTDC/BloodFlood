using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            StartCoroutine(Reflect(collision));
        }
    }

    public IEnumerator Reflect(Collider2D bullet)
    {
        audioManager.PlaySFX(audioManager.playerSwordSwing);
        //Time.timeScale = 0.2f;
        bullet.GetComponent<Projectile>().Flip();
        yield return new WaitForSeconds(0.1f);
        //Time.timeScale = 1;
    }
}
