using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 0f;
    public int bulletDamage;
    public Rigidbody2D rb;
    public bool followsPlayer;
    private GameObject player;
    private bool facingLeft = true;

    public Transform[] firePoints;

    void Start()
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
    }

    public void Flip()
    {
        facingLeft = !facingLeft;

        transform.Rotate(0f, 180f, 0f);
    }

}