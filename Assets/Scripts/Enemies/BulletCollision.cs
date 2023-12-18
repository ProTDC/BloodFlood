using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public BossBullet bullet;
    public Projectile projectile;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BossBullet"))
        {
           Destroy(collision);
        }

        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision);
        }
    }
}
