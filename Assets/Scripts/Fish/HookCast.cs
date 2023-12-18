using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookCast : MonoBehaviour
{
    public Rigidbody2D rb;
    public FishingRod rod;
    private lineRendererScript line;
    public float speed = 0f;
    public bool hitWater = true;

    private void Awake()
    {
        rod = GameObject.Find("FishingRod").GetComponent<FishingRod>();
        line = GetComponent<lineRendererScript>();
    }

    void Start()
    {
        rb.velocity = transform.right * speed;
        line.DrawLineBetweenObjects(transform, rod.firePoint.transform);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Water"))
        {
            rod.isHooked = true;
            rod.elapsedTime = 0f;
        }
        else
        {
            rod.canShoot = true;
            rod.casted = false;
            rod.slider.value = 0f;

            Destroy(gameObject);
        }

        if (collision.transform.CompareTag("Item"))
        {
            rod.fishSpawned = true;
        }
    }
}
