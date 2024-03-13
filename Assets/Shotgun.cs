using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
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

            int burstSize = 6;

            for (int i = 0; i < burstSize; i++)
            {
                float minRotation = -7f;
                float maxRotation = 7f;
                float randomRotation = Random.Range(minRotation, maxRotation);

                Quaternion bulletRotation = Quaternion.Euler(0, 0, randomRotation);

                GameObject.Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * bulletRotation);
            }

            anim.SetTrigger("ShotgunShoot");
            player.currentBullets--;
        }
        else
        {
            return;
        }
    }

    //IEnumerator Reload()
    //{
    //    yield return new WaitForSeconds(shootingCooldown);
    //    player.currentBullets = player.maxBullets;
    //}
}
