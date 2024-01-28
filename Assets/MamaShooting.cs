using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class MamaShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform playerTransform;
    public GameObject player;
    public Transform[] firePoint;
    public Animator animator;
    private int bulletsAmount;

    public float range;
    public float timeBtwShots;
    public bool isFlipped = true;

    private float timer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        LookAtPlayer();

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < range)
        {
            timer += Time.deltaTime;

            if (timer > 2)
            {
                animator.SetTrigger("Shoot");
                timer = 0;
            }
        }

    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.transform.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.transform.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    public void Shoot()
    {
        for (int i = 0; i < firePoint.Length; i++)
        {
            GameObject.Instantiate(bullet, firePoint[i].position, firePoint[i].rotation);
        }

    }
}
