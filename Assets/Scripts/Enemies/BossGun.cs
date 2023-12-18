using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGun : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float cooldown = 0.5f;
    public int hasBullets = 1;
    public bool canShoot = true;

    public static BossGun instance;
    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (hasBullets != 0)
        {
            canShoot = true;
        }
        else
        {
            canShoot = false;
            StartCoroutine(Reload());
        }
    }
    public void Shoot()
    {
        if (canShoot == true)
        {
            GameObject.Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            canShoot = false;

            hasBullets--;
        }
        else
        {
            return;
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(cooldown);
        hasBullets = 1;
    }
}
