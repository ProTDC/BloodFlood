using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganIgnoreCollision : MonoBehaviour
{
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
    //    {
    //        Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
    //    }
    //}

    private void FixedUpdate()
    {
        Physics2D.IgnoreLayerCollision(15, 10);
    }
}
