using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public int bulletDamage;
    public Rigidbody2D rb;
    public bool followsPlayer;
    private GameObject player;
    private bool facingLeft = true;
    private bool canHurtEnemies = false;
    private AudioManager audioManager;

    public GameObject explosionParticle;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        //if (followsPlayer)
        //{
        //    player = GameObject.FindGameObjectWithTag("Player");

        //    Vector3 direction = player.transform.position - transform.position;
        //    rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;

        //    float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        //    transform.rotation = Quaternion.Euler(0, 0, rot);
        //}
        //else
        //{
        //    rb.velocity = transform.right * speed;
        //}
    }

    private void FixedUpdate()
    {
        if (followsPlayer)
        {
            player = GameObject.FindGameObjectWithTag("Player");

            Vector3 direction = player.transform.position - transform.position;
            rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;

            float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rot);
        }
        else
        {
            rb.velocity = transform.right * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();

        if (player != null)
        {
            player.Damage(bulletDamage);
        }

        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("Enemy") && canHurtEnemies)
        {
            Debug.Log("Collided with enemy!");
            EnemyHealth enemy = collision.GetComponent<EnemyHealth>();

            if (enemy != null)
            {
                Instantiate(explosionParticle, transform.position, Quaternion.identity);
                enemy.Damage(bulletDamage, transform.gameObject);
                audioManager.PlaySFX(audioManager.playerDying);
            }
        }
    }

    public void Flip()
    {
        canHurtEnemies = true;

        rb.velocity = -rb.velocity * 2f;

        transform.Rotate(0f, 180f, 0f);

    }

}
