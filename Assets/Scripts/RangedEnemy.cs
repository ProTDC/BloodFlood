using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public GameObject bullet;
    public Transform playerTransform;
    public GameObject player;
    public Transform firePoint;
    public Animator animator;

    public float range; 
    public float timeBtwShots;
    public float accuracy;
    public float shootingAngle;
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

            if (timer >= timeBtwShots)
            {
                animator.Play("Shoot");
                timer = 0;

                Quaternion shotRotation = firePoint.rotation * Quaternion.Euler(0, 0, Random.Range(-5, 5) * accuracy);
            }

            //Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
            
            //float angleToPlayer = Vector3.Angle(transform.right, directionToPlayer);
   
            //if (angleToPlayer <= shootingAngle)
            //{
            //    timer += Time.deltaTime;

            //    if (timer >= timeBtwShots)
            //    {
            //        animator.Play("Shoot");
            //        timer = 0;

            //        Quaternion shotRotation = firePoint.rotation * Quaternion.Euler(0, 0, Random.Range(-5, 5) * accuracy);
            //    }
            //}
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
        GameObject.Instantiate(bullet, firePoint.position, firePoint.rotation);
    }
}
