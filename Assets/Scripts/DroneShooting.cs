using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform playerTransform;
    public GameObject player;
    public Transform firePoint1;
    public Transform firePoint2;
    public Transform firePoint3;
    public Animator animator;

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
                animator.SetTrigger("Attack");
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
        GameObject.Instantiate(bullet, firePoint1.position, firePoint1.rotation);
        GameObject.Instantiate(bullet, firePoint2.position, firePoint2.rotation);
        GameObject.Instantiate(bullet, firePoint3.position, firePoint3.rotation);
    }
}
