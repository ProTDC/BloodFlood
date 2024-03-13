using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Animator anim;
    public PlayerMovement player;
    public float shootingCooldown = 2f;

    public bool canShoot;

    void Update()
    {
        if (player.currentBullets != 0)
        {
            canShoot = true;
        }
        else
        {
            canShoot = false;
            //StartCoroutine(Reload());
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (canShoot == true)
        {
            var audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
            audio.PlaySFX(audio.playerShooting);
            int burstSize = 3;

            for (int i = 0; i < burstSize; i++)
            {
                float delayBetweenBullets = 0.1f;
                StartCoroutine(InstantiateBulletWithDelay(i * delayBetweenBullets));
            }
            anim.SetTrigger("Shoot");

            player.currentBullets--;
        }
        else
        {
            return;
        }
    }

    IEnumerator InstantiateBulletWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        float minRotation = -5f;
        float maxRotation = 5f;

        float randomRotation = Random.Range(minRotation, maxRotation);

        Quaternion bulletRotation = Quaternion.Euler(0, 0, randomRotation);

        GameObject.Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * bulletRotation);
    }

    //IEnumerator Reload()
    //{
    //    yield return new WaitForSeconds(shootingCooldown);
    //    player.currentBullets = player.maxBullets;
    //}
}
