using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Animator anim;
    public PlayerMovement player;
    public float shootingCooldown = 2f;

    public bool canShoot;

    void Start()
    {
        player = GetComponent<PlayerMovement>();
    }

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
            GameObject.Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            anim.SetTrigger("Shoot");

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
