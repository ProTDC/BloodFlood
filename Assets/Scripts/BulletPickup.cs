using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPickup : MonoBehaviour
{
    private PlayerMovement player;
    private void Start()
    {
        player = PlayerMovement.instance;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && player.currentBullets !< player.maxBullets)
        {
            player.currentBullets++;
            Destroy(gameObject);
        }
        else
        {
            return;
        }
    }
}
