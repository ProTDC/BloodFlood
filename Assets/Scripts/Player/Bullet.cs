using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 0f;
    public int bulletDamage = 1;
    public Rigidbody2D rb;
    public float destroyDelay = 5f;
    public bool canStun = true;

    void Start()
    {
        rb.velocity = transform.right * speed;

        Destroy(gameObject, destroyDelay);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();

        if (enemy != null)
        {
            enemy.Damage(bulletDamage, transform.gameObject);

            //if (canStun)
            //{
            //    StartCoroutine(StunEnemy(enemy, 2.5f)); 
            //}
        }

        Destroy(gameObject);

        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    //private IEnumerator StunEnemy(EnemyHealth enemy, float stunDuration)
    //{
    //    enemy.Stun(stunDuration);

    //    yield return null;
    //}

}
